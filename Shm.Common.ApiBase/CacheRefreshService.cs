using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiBase;

public class CacheRefreshService(PerRequestContext perRequestContext, IDbManagerFactory dbManagerFactory)
{
    public virtual async Task RefreshAsync(CancellationToken cancellationToken)
    {
        var alertRuleManager = dbManagerFactory.AlertRuleManager;
        var alertRules = await alertRuleManager.GetAsync(true, true, cancellationToken).ConfigureAwait(false);
        perRequestContext.CacheFactory.AlertRuleCache.Refresh(alertRules.Items);

        var bridgeManager = dbManagerFactory.BridgeManager;
        var bridges = await bridgeManager.GetAsync(true, cancellationToken).ConfigureAwait(false);
        perRequestContext.CacheFactory.BridgeCache.Refresh(bridges.Items);

        var daqManager = dbManagerFactory.DaqManager;
        var daqs = await daqManager.GetAsync(true, true, cancellationToken).ConfigureAwait(false);
        perRequestContext.CacheFactory.DaqCache.Refresh(daqs.Items);

        var sensorManager = dbManagerFactory.SensorManager;
        var sensors = await sensorManager.GetAsync(true, true, cancellationToken).ConfigureAwait(false);
        perRequestContext.CacheFactory.SensorCache.Refresh(sensors.Items);

        var sensorOutputManager = dbManagerFactory.SensorOutputManager;
        var sensorOutputs = await sensorOutputManager.GetAsync(true, true, cancellationToken).ConfigureAwait(false);
        perRequestContext.CacheFactory.SensorOutputCache.Refresh(sensorOutputs.Items);
    }
}