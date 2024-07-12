using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Models.ConfigModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;
using Microsoft.Extensions.Options;

namespace BridgeIntelligence.iCompass.Shm.Common.Utilities;

public class PerRequestContext(IApiRequestContext apiRequestContext, 
    IIxLogger logger,
    IOptions<ApiServiceConfigModel> serviceConfig,
    UserContextModel userContext,
    CacheFactory cacheFactory)
{
    public UserContextModel UserContext { get; } = userContext;

    public IApiRequestContext ApiRequestContext { get; } = apiRequestContext;

    public ApiServiceConfigModel ServiceConfig { get; } = serviceConfig.Value;

    public IIxLogger Logger { get; set; } = logger;

    public CacheFactory CacheFactory { get; } = cacheFactory;
}