using BridgeIntelligence.iCompass.Shm.Common.ApiBase;
using BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;
using BridgeIntelligence.iCompass.Shm.Management.Api.Cache;
using BridgeIntelligence.iCompass.Shm.Management.Api.Models;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using BridgeIntelligence.iCompass.Shm.Management.Api.Workers;

namespace BridgeIntelligence.iCompass.Shm.Management.Api;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBaseServices(configuration, typeof(Startup));

        services.AddScoped<AlertRuleService>();
        services.AddScoped<BridgeService>();
        services.AddScoped<DaqService>();
        services.AddScoped<LookupsService>();
        services.AddScoped<SensorOutputService>();
        services.AddScoped<SensorService>();
        services.AddScoped<ServiceService>();
        services.AddScoped<WatchdogQueryService>();

        services.AddSingleton<PointDataCache>();
        services.AddSingleton<WindowDataCache>();
        services.AddScoped<ShmCacheRefreshService>();
        services.AddScoped<CacheRefreshService, ShmCacheRefreshService>();
        services.AddSingleton<ShmCacheFactory>();
        services.AddSingleton<CacheFactory, ShmCacheFactory>();
        services.Configure<DashboardConfigModel>(configuration.GetSection("Dashboard"));

        if (configuration.GetValue<bool>("General:Service:IsDebug"))
        {
            services.AddScoped<IDashboardService, TestDashboardService>();
        }
        else
        {
            services.AddHostedService<DashboardSensorDataStreamWorker>();
            services.AddScoped<IDashboardService, DashboardService>();
        }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(WebApplication app)
    {
        app.ConfigureBasePipeline();
    }
}