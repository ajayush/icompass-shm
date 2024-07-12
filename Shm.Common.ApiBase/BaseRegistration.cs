using System.Reflection;
using BridgeIntelligence.CommonCore.ApiBase.ExceptionHandling;
using BridgeIntelligence.CommonCore.ApiBase.Swagger;
using BridgeIntelligence.CommonCore.ApiBase;
using BridgeIntelligence.CommonCore.EventPipeline.Models;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;
using BridgeIntelligence.iCompass.Shm.Common.DbCore;
using BridgeIntelligence.iCompass.Shm.Common.EventLogger;
using BridgeIntelligence.iCompass.Shm.Common.EventLogger.Redis;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiBase;

public static class BaseRegistration
{
    public static void ConfigureBaseServices(this IServiceCollection services, IConfiguration configuration, Type type)
    {
        // MVC Application setup
        services.AddControllersWithViews(options => { options.Filters.Add(new BIExceptionFilter()); })
            .AddNewtonsoftJson(options => { options.UseMemberCasing(); })
            .ConfigureApiBehaviorOptions(options =>
                options.InvalidModelStateResponseFactory = ExceptionResponseHelper.ValidateModelState);

        // TODO: Remove this once the UserContextModel is implemented in the base library
        services.AddScoped(_ => new UserContextModel());
        // Base services
        services.RegisterIxServices(new IxRegistrationOptions
        {
            Configuration = configuration,
            EnableIISDeployment = true,
            EnableLogging = true
        });

        var serviceName = configuration.GetValue<string>("General:Service:Name");

        // Swagger
        services.AddCustomSwagger(options =>
        {
            options.Title = $"SDK for {serviceName}";
            options.Description = $"SDK for {serviceName}";
            options.XmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                type.GetTypeInfo().Assembly.GetName().Name + ".xml");

            options.CustomStylesheetFilePath = "/swagger/swagger-custom.css";
            options.RoutePrefix = "";
        });

        if (string.IsNullOrWhiteSpace(configuration.GetValue<string>("EventLogger:ConnectionString")))
        {
            services.AddSingleton<IDataEventLogger<EventLogModel>, NullShmDataEventLogger>();
            services.AddSingleton<IDataEventLogger<BatchDataModel>, NullSensorDataEventLogger>();
        }
        else
        {
            services.AddSingleton<ConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration.GetValue<string>("EventLogger:ConnectionString") ?? "localhost"));
            services.Configure<RedisConfig>(configuration.GetSection("EventLogger"));
            services.AddSingleton<IDataEventLogger<EventLogModel>, RedisShmDataEventLogger>();
            services.AddSingleton<IDataEventLogger<BatchDataModel>, RedisSensorDataEventLogger>();
        }

        services.AddHttpContextAccessor();
        services.AddScoped<PerRequestContext>();
        services.AddScoped<UserContextModel, ShmUserContext>();
        services.AddDbManagers(configuration);

        services.AddSingleton<CacheFactory>();
        services.AddSingleton<BridgeCache>();
        services.AddSingleton<DaqCache>();
        services.AddSingleton<SensorCache>();
        services.AddSingleton<SensorOutputCache>();
        services.AddSingleton<AlertRuleCache>();
        services.AddScoped<CacheRefreshService>();
    }

    public static void ConfigureBasePipeline(this WebApplication app)
    {
        app.Use((context, next) =>
        {
            context.Request.Scheme = "https";
            return next();
        });

        app.RegisterIxMiddleware();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHsts();
        }

        // Allows access to HTML/JS/CSS files
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseCustomSwagger();

        // Use routing for the application, allowing for annotations on Controllers and Actions with Route Attribute
        app.UseRouting();
        app.UseAuthentication();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        ConfigureForStartupAsync(app, CancellationToken.None).Wait();
    }

    private static async Task ConfigureForStartupAsync(this WebApplication app, CancellationToken cancellationToken)
    {
        using var scope = app.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<CacheRefreshService>().RefreshAsync(cancellationToken).ConfigureAwait(false);
    }
}