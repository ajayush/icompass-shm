namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;

public class DashboardEventDataReturnModel
{
    public List<DashboardWindowDataModel> WindowAverageData { get; set; } = new();

    public List<DashboardPointDataModel> PointData { get; set; } = new();
}