namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;

public enum NotificationDeliveryStatus
{
    Initiated,
    Delivered,
    Failed,
    SkippedDueToEmailNotEnabled,
    SkippedToAvoidDuplication
}