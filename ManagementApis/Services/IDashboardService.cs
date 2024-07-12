using BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public interface IDashboardService
{
    BurlingtonDashboardModel GetForBurlington();
    TaconyDashboardModel GetTaconyData();
    RiversideDashboardModel GetRiversideData();
}