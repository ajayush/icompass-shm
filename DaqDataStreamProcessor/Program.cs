using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api;

var builder = WebApplication.CreateBuilder(args);
Startup.ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
Startup.Configure(app);
app.Run();