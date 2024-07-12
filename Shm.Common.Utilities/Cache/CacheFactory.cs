namespace BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;

public class CacheFactory(BridgeCache bridgeCache,
    DaqCache daqCache,
    SensorCache sensorCache,
    SensorOutputCache sensorOutputCache,
    AlertRuleCache alertRuleCache)
{
    public BridgeCache BridgeCache { get; } = bridgeCache;
    
    public DaqCache DaqCache { get; } = daqCache;
    
    public SensorCache SensorCache { get; } = sensorCache;
    
    public SensorOutputCache SensorOutputCache { get; } = sensorOutputCache;
    
    public AlertRuleCache AlertRuleCache { get; } = alertRuleCache;
}