using BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore;

public class DbManagerFactory(PerRequestContext perRequestContext, ShmDbContext dbContext) : IDbManagerFactory
{
    public IAlertRuleEmailNotificationManager AlertRuleEmailNotificationManager => new AlertRuleEmailNotificationManager(perRequestContext, dbContext);
    public IAlertRuleManager AlertRuleManager => new AlertRuleManager(perRequestContext, dbContext);
    public IBridgeManager BridgeManager => new BridgeManager(perRequestContext, dbContext);
    public IDaqManager DaqManager => new DaqManager(perRequestContext, dbContext);
    public ILookupsManager LookupsManager => new LookupsManager(perRequestContext, dbContext);
    public ISensorManager SensorManager => new SensorManager(perRequestContext, dbContext);
    public ISensorOutputManager SensorOutputManager => new SensorOutputManager(perRequestContext, dbContext);
    public IServiceManager ServiceManager => new ServiceManager(perRequestContext, dbContext);
    public ITableStateManager TableStateManager => new TableStateManager(perRequestContext, dbContext);
    public IWatchdogQueryManager WatchdogQueryManager => new WatchdogQueryManager(perRequestContext, dbContext);
}