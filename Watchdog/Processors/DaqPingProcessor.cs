using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;

namespace BridgeIntelligence.iCompass.Shm.Watchdog.Api.Processors;

public class DaqPingProcessor(PerRequestContext perRequestContext,
    IDbManagerFactory dbManagerFactory,
    IHttpClientFactory httpClientFactory)
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var daqs = await dbManagerFactory.DaqManager.GetAsync(false, false, cancellationToken).ConfigureAwait(false);
        foreach (var daq in daqs.Items)
        {
            if (string.IsNullOrWhiteSpace(daq.PingUrl))
            {
                continue;
            }

            try
            {
                var response = await PingAsync(daq.PingUrl, cancellationToken).ConfigureAwait(false);
                await dbManagerFactory.DaqManager.HandlePingStatusAsync(daq.Id, response, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                perRequestContext.Logger.LogHandledException(e);
            }
        }
    }

    private async Task<bool> PingAsync(string serviceUrl, CancellationToken cancellationToken)
    {
        try
        {
            using var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(serviceUrl, cancellationToken).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}