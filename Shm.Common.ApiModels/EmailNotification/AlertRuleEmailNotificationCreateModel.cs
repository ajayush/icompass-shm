using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.EmailNotification;

public class AlertRuleEmailNotificationCreateModel
{
    public string To { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;

    public int AlertRuleId { get; set; }

    public NotificationDeliveryStatus DeliveryStatus { get; set; }
}