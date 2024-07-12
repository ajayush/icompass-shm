namespace BridgeIntelligence.iCompass.Shm.Management.Api.Models;

public class DashboardDataConfigModel
{
    public BridgeNames BridgeName { get; set; } = default!;

    public string DataName { get; set; } = default!;

    public string TableName { get; set; } = default!;

    public string ColumnName { get; set; } = default!;

    public DashboardDataTypes DataType { get; set; } = default!;

    public int? MaxIntervalInSeconds { get; set; }

    public int MaxNumberOfPoints { get; set; }
}