using BridgeIntelligence.iCompass.Shm.Watchdog.Api.Models;
using BridgeIntelligence.iCompass.Shm.Watchdog.Api.Processors;
using Microsoft.Extensions.Options;

namespace BridgeIntelligence.iCompass.Shm.Watchdog.Api.Workers;

public class PingWorker(IOptions<PingerConfigModel> pingerConfig, IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await RunSingleAsync(cancellationToken).ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromSeconds(pingerConfig.Value.IntervalInSeconds), cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task RunSingleAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        if (pingerConfig.Value.EnabledForDaqs)
        {
            await scope.ServiceProvider.GetRequiredService<DaqPingProcessor>().RunAsync(cancellationToken);
        }

        if (pingerConfig.Value.EnabledForServices)
        {
            await scope.ServiceProvider.GetRequiredService<ServicePingProcessor>().RunAsync(cancellationToken);
        }
    }
}