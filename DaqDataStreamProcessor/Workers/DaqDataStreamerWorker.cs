using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;
using Microsoft.Extensions.Options;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Workers;

internal class DaqDataStreamerWorker(IIxSingletonLogger logger, 
    IOptions<WorkerIterationConfigModel> workerIterationConfig, 
    IMetadataCache metadataCache,
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var nextRefreshTime = DateTimeOffset.UtcNow.AddDays(-10);
                while (!cancellationToken.IsCancellationRequested)
                {
                    // This is to individually track each loop of the worker
                    logger.LogInformation("Running next cycle for parsing data");

                    if (nextRefreshTime < DateTimeOffset.UtcNow)
                    {
                        logger.LogInformation("Refreshing meta information");
                        await RefreshMetadataAsync(cancellationToken).ConfigureAwait(false);
                        nextRefreshTime = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(_workerIterationConfig.MetaRefreshIntervalInSeconds));
                    }

                    await ProcessDataAsync(cancellationToken).ConfigureAwait(false);

                    // Sleep and wake up after the configured seconds for the next iteration
                    await Task.Delay(TimeSpan.FromSeconds(_workerIterationConfig.DataParseIntervalInSeconds), cancellationToken)
                        .ConfigureAwait(false);
                }
            }
        }
        catch (Exception e)
        {
            // Handle exceptions here
            logger.LogHandledException(e);
        }
    }

    private async Task ProcessDataAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dataProcessor = scope.ServiceProvider.GetRequiredService<IDataProcessor>();
        await dataProcessor.ParseDataAsync(metadataCache.GetListOfColumns(), cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task RefreshMetadataAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var sensorOutputManager = scope.ServiceProvider.GetRequiredService<ISensorOutputManager>();
        var sensorOutputs = await sensorOutputManager.GetAsync(false, false, cancellationToken).ConfigureAwait(false);
        metadataCache.Refresh(sensorOutputs.Items.OrderBy(a=>a.Id).Skip(0).Take(_workerIterationConfig.MaxSensorsToParse).ToList());
    }

    private readonly WorkerIterationConfigModel _workerIterationConfig = workerIterationConfig.Value;
}