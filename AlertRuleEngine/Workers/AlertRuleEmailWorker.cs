using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Config;
using Microsoft.Extensions.Options;

namespace BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Workers;

public class AlertRuleEmailWorker(IIxSingletonLogger logger, 
    IServiceProvider serviceProvider,
    IOptions<AlertRuleWorkerConfig> workerConfig) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            logger.LogInformation("Running alert rule email worker");
            await RunAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Completed alert rule email worker");

            await Task.Delay(TimeSpan.FromSeconds(workerConfig.Value.CycleIntervalInSeconds), cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var alertRuleEmailProcessor = scope.ServiceProvider.GetRequiredService<AlertRuleEmailProcessor>();
        await alertRuleEmailProcessor.RunAsync(cancellationToken).ConfigureAwait(false);
    }
}