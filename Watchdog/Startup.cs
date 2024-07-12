using BridgeIntelligence.iCompass.Shm.Common.ApiBase;
using BridgeIntelligence.iCompass.Shm.Watchdog.Api.Models;
using BridgeIntelligence.iCompass.Shm.Watchdog.Api.Workers;

namespace BridgeIntelligence.iCompass.Shm.Watchdog.Api;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBaseServices(configuration, typeof(Startup));
        services.AddHostedService<PingWorker>();
        services.Configure<PingerConfigModel>(configuration.GetSection("PingerConfig"));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(WebApplication app)
    {
        app.ConfigureBasePipeline();
    }
}