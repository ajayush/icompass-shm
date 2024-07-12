using BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Cache;

public class ShmCacheFactory(BridgeCache bridgeCache,
        DaqCache daqCache,
        SensorCache sensorCache,
        SensorOutputCache sensorOutputCache,
        AlertRuleCache alertRuleCache,
        WindowDataCache windowDataCache,
        PointDataCache pointDataCache)
    : CacheFactory(bridgeCache, daqCache, sensorCache, sensorOutputCache, alertRuleCache)
{
    public WindowDataCache WindowDataCache { get; } = windowDataCache;

    public PointDataCache PointDataCache { get; } = pointDataCache;
}