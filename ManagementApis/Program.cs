/*
 * The service is responsible for providing the UI with the necessary data to display the SHM data in a user-friendly way.
 * 
 */

using BridgeIntelligence.iCompass.Shm.Management.Api;

var builder = WebApplication.CreateBuilder(args);
Startup.ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
Startup.Configure(app);
app.Run();