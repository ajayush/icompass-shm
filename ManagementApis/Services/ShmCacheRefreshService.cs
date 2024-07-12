using BridgeIntelligence.CommonCore.Utilities;
using BridgeIntelligence.iCompass.Shm.Common.ApiBase;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using BridgeIntelligence.iCompass.Shm.Management.Api.Cache;
using BridgeIntelligence.iCompass.Shm.Management.Api.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class ShmCacheRefreshService(PerRequestContext perRequestContext, IDbManagerFactory dbManagerFactory, IOptions<DashboardConfigModel> dashboardConfig)
    : CacheRefreshService(perRequestContext, dbManagerFactory)
{
    public override async Task RefreshAsync(CancellationToken cancellationToken)
    {
        var fileContents = await File.ReadAllTextAsync(PathExtensions.GetDirectoryPathForBin(dashboardConfig.Value.ConfigFilePath), cancellationToken).ConfigureAwait(false);
        var dashboardConfigModel = JsonConvert.DeserializeObject<List<DashboardDataConfigModel>>(fileContents)!;
        var cacheFactory = perRequestContext.CacheFactory as ShmCacheFactory;
        if (cacheFactory == null)
        {
            throw new Exception("Cache factory is not of type ShmCacheFactory");
        }

        cacheFactory.PointDataCache.Initialize(dashboardConfigModel);
        cacheFactory.WindowDataCache.Initialize(dashboardConfigModel);
        await base.RefreshAsync(cancellationToken).ConfigureAwait(false);
    }
}