namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;

public class WatchdogAlertQueryModel
{
    public int? BridgeId { get; set; }

    public int? DaqId { get; set; }

    public int? SensorId { get; set; }

    public string? System { get; set; }

    public DateTimeOffset? DateFrom { get; set; }

    public DateTimeOffset? DateTo { get; set; }
}

public class AlertRuleFailureQueryModel
{
    public int? BridgeId { get; set; }

    public int? DaqId { get; set; }

    public int? SensorId { get; set; }

    public int? AlertRuleId { get; set; }

    public DateTimeOffset? StartTime { get; set; }

    public DateTimeOffset? EndTime { get; set; }

    public bool? IsNotificationSentForFailure { get; set; }
}