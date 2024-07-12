using BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Config;
using BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Workers;
using BridgeIntelligence.iCompass.Shm.Common.ApiBase;

namespace BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBaseServices(configuration, typeof(Startup));

        services.AddHostedService<AlertRuleSensorDataStreamWorker>();
        services.AddHostedService<AlertRuleEmailWorker>();
        services.AddScoped<AlertRuleEmailProcessor>();
        services.Configure<AlertRuleWorkerConfig>(configuration.GetSection("AlertRuleWorker"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(WebApplication app)
    {
        app.ConfigureBasePipeline();
    }
}