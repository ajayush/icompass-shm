using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.EmailNotification;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface IAlertRuleEmailNotificationManager
{
    Task<AlertRuleEmailNotificationReturnModel> CreateAsync(AlertRuleEmailNotificationCreateModel model,
        CancellationToken cancellationToken);

    Task<AlertRuleEmailNotificationReturnModel> UpdateDeliveryStatusAsync(int id,
        NotificationDeliveryStatus deliveryStatus, CancellationToken cancellationToken);

    Task<AlertRuleEmailNotificationReturnModel?> GetLatestForAlertAsync(int alertId,
        CancellationToken cancellationToken);
}