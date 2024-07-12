namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;

public class DashboardPointDataModel
{
    public string TableName { get; set; } = default!;

    public string ColumnName { get; set; } = default!;

    public DateTime? TimeOfPoint { get; set; }

    public float? PointValue { get; set; }

    public string DataName { get; set; } = default!;

    public string BridgeName { get; set; } = default!;
}