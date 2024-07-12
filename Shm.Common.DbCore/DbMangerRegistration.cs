using BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore;

public static class DbMangerRegistration
{
    public static IServiceCollection AddDbManagers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAlertRuleEmailNotificationManager, AlertRuleEmailNotificationManager>();
        services.AddScoped<IAlertRuleManager, AlertRuleManager>();
        services.AddScoped<IBridgeManager, BridgeManager>();
        services.AddScoped<IDaqManager, DaqManager>();
        services.AddScoped<ILookupsManager, LookupsManager>();
        services.AddScoped<ISensorManager, SensorManager>();
        services.AddScoped<ISensorOutputManager, SensorOutputManager>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<ITableStateManager, TableStateManager>();
        services.AddScoped<IWatchdogQueryManager, WatchdogQueryManager>();
        services.AddScoped<IDbManagerFactory, DbManagerFactory>();
        services.AddDbContext<ShmDbContext>(options =>
            options.UseSqlServer(configuration.GetValue<string>("ShmDatabase:ConnectionString"),
                builder => builder.EnableRetryOnFailure(5)));
        return services;
    }
}