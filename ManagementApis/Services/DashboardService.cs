using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using BridgeIntelligence.iCompass.Shm.Management.Api.Cache;
using BridgeIntelligence.iCompass.Shm.Management.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class DashboardService(PerRequestContext perRequestContext) : IDashboardService
{
    public BurlingtonDashboardModel GetForBurlington()
    {
        perRequestContext.Logger.LogTrace();

        if (perRequestContext.CacheFactory is not ShmCacheFactory shmCache)
        {
            throw ExceptionHelper.InternalServerException("CacheFactory is not of type ShmCacheFactory");
        }

        var dashboardWorkerModel = new BurlingtonDashboardModel
        {
            AirTempHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "AirTempHistory") ?? new DashboardWindowDataModel(),
            WindSpeedHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "WindSpeedHistory") ?? new DashboardWindowDataModel(),
            AirGapHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "AirGapHistory") ?? new DashboardWindowDataModel(),
            AmbientAirTemperature = shmCache.PointDataCache.Get(BridgeNames.Burlington.ToString(), "AmbientAirTemperature") ?? new DashboardPointDataModel(),
            NjApproachSurfaceTemperature = shmCache.PointDataCache.Get(BridgeNames.Burlington.ToString(), "NjApproachSurfaceTemperature") ?? new DashboardPointDataModel(),
            PaApproachSurfaceTemperature = shmCache.PointDataCache.Get(BridgeNames.Burlington.ToString(), "PaApproachSurfaceTemperature") ?? new DashboardPointDataModel(),
            AirGap = shmCache.PointDataCache.Get(BridgeNames.Burlington.ToString(), "AirGap") ?? new DashboardPointDataModel(),
            WindDirection = shmCache.PointDataCache.Get(BridgeNames.Burlington.ToString(), "WindDirection") ?? new DashboardPointDataModel(),
            WindSpeed = shmCache.PointDataCache.Get(BridgeNames.Burlington.ToString(), "WindSpeed") ?? new DashboardPointDataModel(),
            BridgeName = BridgeNames.Burlington.ToString()
        };


        return dashboardWorkerModel;
    }

    public TaconyDashboardModel GetTaconyData()
    {
        perRequestContext.Logger.LogTrace();

        if (perRequestContext.CacheFactory is not ShmCacheFactory shmCache)
        {
            throw ExceptionHelper.InternalServerException("CacheFactory is not of type ShmCacheFactory");
        }

        var taconyDashboardModel = new TaconyDashboardModel
        {
            WindDirection = shmCache.PointDataCache.Get(BridgeNames.Tacony.ToString(), "WindDirection") ?? new DashboardPointDataModel(),
            NjApproachSurfaceTemperature = shmCache.PointDataCache.Get(BridgeNames.Tacony.ToString(), "NjApproachSurfaceTemperature") ?? new DashboardPointDataModel(),
            PaApproachSurfaceTemperature = shmCache.PointDataCache.Get(BridgeNames.Tacony.ToString(), "PaApproachSurfaceTemperature") ?? new DashboardPointDataModel(),
            AmbientAirTemperature = shmCache.PointDataCache.Get(BridgeNames.Tacony.ToString(), "AmbientAirTemperature") ?? new DashboardPointDataModel(),
            AirTempHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "AirTempHistory") ?? new DashboardWindowDataModel(),
            WindSpeedHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "WindSpeedHistory") ?? new DashboardWindowDataModel(),
            AirGapHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "AirGapHistory") ?? new DashboardWindowDataModel(),
            AirGap = shmCache.PointDataCache.Get(BridgeNames.Tacony.ToString(), "AirGap") ?? new DashboardPointDataModel(),
            WindSpeed = shmCache.PointDataCache.Get(BridgeNames.Tacony.ToString(), "WindSpeed") ?? new DashboardPointDataModel(),
            BridgeName = BridgeNames.Tacony.ToString()
        };

        return taconyDashboardModel;
    }

    public RiversideDashboardModel GetRiversideData()
    {
        perRequestContext.Logger.LogTrace();

        if (perRequestContext.CacheFactory is not ShmCacheFactory shmCache)
        {
            throw ExceptionHelper.InternalServerException("CacheFactory is not of type ShmCacheFactory");
        }

        var riversideDashboardModel = new RiversideDashboardModel
        {
            PaApproachSurfaceTemperature = shmCache.PointDataCache.Get(BridgeNames.Riverside.ToString(), "PaApproachSurfaceTemperature") ?? new DashboardPointDataModel(),
            AmbientAirTemperature = shmCache.PointDataCache.Get(BridgeNames.Riverside.ToString(), "AmbientAirTemperature") ?? new DashboardPointDataModel(),
            AirTempHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "AirTempHistory") ?? new DashboardWindowDataModel(),
            WindSpeedHistory = shmCache.WindowDataCache.Get(BridgeNames.Burlington.ToString(), "WindSpeedHistory") ?? new DashboardWindowDataModel(),
            WindSpeed = shmCache.PointDataCache.Get(BridgeNames.Riverside.ToString(), "WindSpeed") ?? new DashboardPointDataModel(),
            WindDirection = shmCache.PointDataCache.Get(BridgeNames.Riverside.ToString(), "WindDirection") ?? new DashboardPointDataModel(),
            BridgeName = BridgeNames.Riverside.ToString()
        };
        return riversideDashboardModel;
    }
}