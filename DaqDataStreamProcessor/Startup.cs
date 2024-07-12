using BridgeIntelligence.iCompass.Shm.Common.ApiBase;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Workers;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBaseServices(configuration, typeof(Startup));
        var isDebug = configuration.GetValue<bool>("General:Service:IsDebug");
        if (isDebug)
        {
            services.AddScoped<IDataProcessor, TestDataProcessor>();
            services.AddSingleton<IMetadataCache, TestMetadataCache>();
        }
        else
        {
            services.AddScoped<IDataProcessor, DbDataProcessor>();
            services.AddSingleton<IMetadataCache, DbMetadataCache>();
        }

        services.Configure<WorkerIterationConfigModel>(configuration.GetSection("WorkerIteration"));
        services.Configure<SensorDataConfigModel>(configuration.GetSection("SensorDatabase"));

        services.AddHostedService<DaqDataStreamerWorker>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(WebApplication app)
    {
        app.ConfigureBasePipeline();
    }
}