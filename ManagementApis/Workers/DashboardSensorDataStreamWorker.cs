using BridgeIntelligence.CommonCore.EventPipeline.Models;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models.ConfigModels;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;
using BridgeIntelligence.iCompass.Shm.Common.EventLogger;
using BridgeIntelligence.iCompass.Shm.Management.Api.Cache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Workers;

public class DashboardSensorDataStreamWorker(IIxSingletonLogger logger,
    IDataEventLogger<BatchDataModel> sensorEventLogger,
    IDataEventLogger<EventLogModel> shmEventLogger,
    IOptions<ApiServiceConfigModel> apiServiceConfig,
    WindowDataCache windowDataCache,
    PointDataCache pointDataCache) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // Initialize the message receiver and start receiving the messages
        Receive(cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Still running");
            await Task.Delay(TimeSpan.FromMinutes(60), cancellationToken).ConfigureAwait(false);
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
    /// <param name="data">Data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
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
                pointDataCache.Add(key.DataName, key.Values, data.Timestamps);
                windowDataCache.Add(key.DataName, key.Values, data.Timestamps);
            }

            // TODO: Send message only when a point is updated
            await SendDataAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            logger.LogHandledException(e);
        }
    }

    private async Task SendDataAsync(CancellationToken cancellationToken)
    {
        var dashboardEventDataReturnModel = new DashboardEventDataReturnModel
        {
            WindowAverageData = windowDataCache.Get().ToList(),
            PointData = pointDataCache.Get().ToList()
        };

        var result = JsonConvert.SerializeObject(dashboardEventDataReturnModel);
        await shmEventLogger.SendMessageAsync(
            new EventLogModel
            {
                DateCreated = DateTimeOffset.UtcNow,
                UserId = null,
                TransactionId = logger.TransactionId,
                ServiceName = apiServiceConfig.Value.Name,
                IsWorker = true,
                Entity = ICompassEntityTypes.Dashboard,
                IsSuccessful = true,
                ActivationCode = null,
                ParentEntityId = null,
                Operation = EventLogOperationTypes.Update,
                EntityId = null,
                NewData = result
            }, cancellationToken).ConfigureAwait(false);
    }
}