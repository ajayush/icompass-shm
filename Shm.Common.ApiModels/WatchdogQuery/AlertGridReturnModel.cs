namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;

public class AlertGridReturnModel
{
    public int AlertRuleId { get; set; }

    public string RuleName { get; set; } = default!;

    public int BridgeId { get; set; }

    public string BridgeName { get; set; } = default!;

    public int DaqId { get; set; }

    public string DaqName { get; set; } = default!;

    public int SensorId { get; set; }

    public string SensorName { get; set; } = default!;

    public DateTimeOffset AlertTime { get; set; }

    public string AlertType { get; set; } = default!;

    public string Action { get; set; } = default!;
}