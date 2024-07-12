using BridgeIntelligence.CommonCore.EmailSender;
using BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Config;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.EmailNotification;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.Extensions.Options;

namespace BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Workers;

public class AlertRuleEmailProcessor(PerRequestContext perRequestContext, 
    IEmailSender emailSender, 
    IDbManagerFactory dbManagerFactory, 
    IOptions<AlertRuleWorkerConfig> workerConfig)
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            await RunCoreAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            perRequestContext.Logger.LogHandledException(e);
        }
    }

    private async Task RunCoreAsync(CancellationToken cancellationToken)
    {
        var failedRules = await dbManagerFactory.AlertRuleManager
            .GetFailuresAsync(new GridQueryModel<AlertRuleFailureQueryModel>
            {
                Skip = 0,
                Take = workerConfig.Value.MaxFailuresInCycle,
                CustomFilters = new AlertRuleFailureQueryModel
                {
                    IsNotificationSentForFailure = false
                }
            }, cancellationToken).ConfigureAwait(false);

        var emailsDict = new Dictionary<string, List<Tuple<AlertRuleFailureReturnModel, AlertRuleReturnModel>>>();

        foreach (var failure in failedRules.Items)
        {
            var alertRule = perRequestContext.CacheFactory.AlertRuleCache.GetAlertWithId(failure.AlertRuleId);
            if (alertRule == null)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(alertRule.ToEmailAddresses))
            {
                continue;
            }

            var emails = alertRule.ToEmailAddresses.Split(',', ';').Where(a => !string.IsNullOrWhiteSpace(a))
                .Select(a => a.Trim().ToLowerInvariant()).ToList();

            foreach (var email in emails)
            {
                if (!emailsDict.ContainsKey(email))
                {
                    emailsDict[email] = new List<Tuple<AlertRuleFailureReturnModel, AlertRuleReturnModel>>();
                }

                emailsDict[email].Add(new Tuple<AlertRuleFailureReturnModel, AlertRuleReturnModel>(failure, alertRule));
            }
        }

        foreach (var email in emailsDict.Keys)
        {
            var lines = new List<string>();
            var isFirst = true;
            foreach (var failure in emailsDict[email])
            {
                var sensorOutput = perRequestContext.CacheFactory.SensorOutputCache.GetWithId(failure.Item2.SensorOutputId);
                if (sensorOutput == null)
                {
                    continue;
                }

                var sensor = perRequestContext.CacheFactory.SensorCache.GetWithId(sensorOutput.SensorId);
                if (sensor == null)
                {
                    continue;
                }

                var daq = perRequestContext.CacheFactory.DaqCache.GetWithId(sensor.DaqId);
                if (daq == null)
                {
                    continue;
                }

                var bridge = perRequestContext.CacheFactory.BridgeCache.GetById(daq.BridgeId);
                if (bridge == null)
                {
                    continue;
                }

                if (isFirst)
                {
                    lines.Add("Following alert rule failures were encountered:");
                    lines.Add(string.Empty);
                }
                else
                {
                    lines.Add(string.Empty);
                    lines.Add(string.Empty);
                }

                lines.Add($"Alert Rule Name: {failure.Item2.RuleName}");
                lines.Add($"Failure Time: {failure.Item1.StartTime}");
                lines.Add($"Bridge Name: {bridge.BridgeName}");
                lines.Add($"DAQ Name: {daq.Name}");
                lines.Add($"Sensor Name: {sensor.Name}");
                lines.Add($"Sensor Output Type: {sensorOutput.OutputType}");
                lines.Add($"Sensor Output Name: {sensorOutput.TableName}.{sensorOutput.ColumnName}");
                lines.Add($"Formula: {failure.Item2.ExpressionString}");
                isFirst = false;
            }

            if (!lines.Any())
            {
                continue;
            }

            var emailModel = new EmailModel
            {
                To = new List<EmailDetail>
                {
                    new()
                    {
                        Email = email,
                        Name = email.Split('@')[0]
                    }
                },
                Subject = "Alert Rule Failures",
                PlainContent = string.Join(string.Empty, lines),
                HtmlContent = string.Join("<br>", lines),
                From = workerConfig.Value.FromEmail
            };

            perRequestContext.Logger.LogInformation($"Sending failure email to {email}");
            await emailSender.SendEmail(emailModel, cancellationToken).ConfigureAwait(false);
        }

        await dbManagerFactory.AlertRuleManager
            .MarkNotifiedForFailureAsync(failedRules.Items.Select(a => a.Id).ToList(), cancellationToken)
            .ConfigureAwait(false);
    }
}