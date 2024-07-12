namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.EmailNotification;

public class AlertRuleEmailNotificationReturnModel
{
    public int Id { get; set; }

    public string To { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;

    public int AlertRuleId { get; set; }

    public DateTimeOffset DateCreated { get; set; }

    public string DeliveryStatus { get; set; } = default!;

}

public class AlertRuleFailureReturnModel
{
    public int Id { get; set; }

    public int AlertRuleId { get; set; }

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset? EndTime { get; set; }

    public bool IsNotificationSentForFailure { get; set; }

    public bool IsNotificationSentForResolved { get; set; }
}