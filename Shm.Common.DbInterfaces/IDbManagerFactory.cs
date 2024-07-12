namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface IDbManagerFactory
{
    IAlertRuleEmailNotificationManager AlertRuleEmailNotificationManager { get; }
    IAlertRuleManager AlertRuleManager { get; }
    IBridgeManager BridgeManager { get; }
    IDaqManager DaqManager { get; }
    ILookupsManager LookupsManager { get; }
    ISensorManager SensorManager { get; }
    ISensorOutputManager SensorOutputManager { get; }
    IServiceManager ServiceManager { get; }
    ITableStateManager TableStateManager { get; }
    IWatchdogQueryManager WatchdogQueryManager { get; }
}