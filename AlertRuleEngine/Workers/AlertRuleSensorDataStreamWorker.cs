using BridgeIntelligence.CommonCore.EventPipeline.Models;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models.ConfigModels;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.EventLogger;
using BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;
using Microsoft.Extensions.Options;
using NCalc;

namespace BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Workers;

internal class AlertRuleSensorDataStreamWorker(IIxSingletonLogger logger, 
    IDataEventLogger<BatchDataModel> sensorEventLogger,
    IDataEventLogger<EventLogModel> shmEventLogger, 
    CacheFactory cacheFactory, 
    IOptions<ApiServiceConfigModel> apiServiceConfig,
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // Initialize the message receiver and start receiving the messages
        Receive(cancellationToken);

        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            await Task.Delay(TimeSpan.FromMinutes(2), cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Method to start the service bus receiver. The connection will be created and a callback will be associated
    /// </summary>
    /// <param name="cancellationToken"></param>
    private void Receive(CancellationToken cancellationToken)
    {
        sensorEventLogger.ReceiveMessages(ProcessMessage, cancellationToken);
    }

    /// <summary>
    /// Handler that will be used to process each message
    /// </summary>
    /// <param name="data">Data to process</param>
    /// <param name="cancellationToken">Cancellation token</param>
    private async Task ProcessMessage(BatchDataModel data, CancellationToken cancellationToken)
    {
        try
        {
            if (data.Timestamps.Count == 0)
            {
                return;
            }

            foreach (var key in data.DataList)
            {
                await RunForAlertRulesAsync(key.DataName, key.Values, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception e)
        {
            logger.LogHandledException(e);
        }
    }

    /// <summary>
    /// This method runs alert rules validation on each data point
    /// </summary>
    /// <param name="column">Column</param>
    /// <param name="data">Data points</param>
    /// <param name="cancellationToken">Cancellation token</param>
    private async Task RunForAlertRulesAsync(string column, List<float?> data, CancellationToken cancellationToken)
    {
        var sensorOutput = cacheFactory.SensorOutputCache.GetByDataName(column);
        if (sensorOutput == null)
        {
            return;
        }

        var alertRules = cacheFactory.AlertRuleCache.GetBySensorOutputId(sensorOutput.Id);
        if (!alertRules.Items.Any())
        {
            return;
        }
        
        foreach (var rule in alertRules.Items)
        {
            if (string.IsNullOrEmpty(rule.ExpressionString))
            {
                continue;
            }

            var expr = new Expression(rule.ExpressionString);
            foreach (var item in data)
            {
                // Evaluate the expression
                expr.Parameters["value"] = item;
                var isFailed = (bool)expr.Evaluate();

                // Check if the rule is already in the same state, either successful or failed
                if (rule.IsFailedState == isFailed)
                {
                    continue;
                }

                // Update the rule state
                var updatedRule = await UpdateFailedStateAsync(rule, isFailed, cancellationToken).ConfigureAwait(false);

                // Update the cache item
                cacheFactory.AlertRuleCache.AddOrUpdate(updatedRule);
            }
        }
    }

    private async Task<AlertRuleReturnModel> UpdateFailedStateAsync(AlertRuleReturnModel rule, bool isFailedState, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var alertRuleManager = scope.ServiceProvider.GetRequiredService<IAlertRuleManager>();
        var retVal = await alertRuleManager.ChangeFailedStateAsync(rule.Id, isFailedState, cancellationToken).ConfigureAwait(false);

        // Send a message
        await shmEventLogger.SendMessageAsync(new EventLogModel
        {
            DateCreated = DateTimeOffset.UtcNow,
            UserId = null,
            TransactionId = logger.TransactionId,
            ServiceName = apiServiceConfig.Value.Name,
            IsWorker = true,
            Entity = ICompassEntityTypes.AlertRule,
            IsSuccessful = isFailedState,
            ActivationCode = null,
            ParentEntityId = rule.SensorOutputId.ToString(),
            Operation = ICompassOperationTypes.RuleEvaluation,
            EntityId = rule.Id.ToString()
        }, cancellationToken).ConfigureAwait(false);

        return retVal;
    }
}