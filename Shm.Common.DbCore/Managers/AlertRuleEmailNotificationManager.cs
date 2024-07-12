using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.EmailNotification;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class AlertRuleEmailNotificationManager
        (PerRequestContext perRequestContext, ShmDbContext dbContext) : IAlertRuleEmailNotificationManager
{
    public async Task<AlertRuleEmailNotificationReturnModel> CreateAsync(AlertRuleEmailNotificationCreateModel model,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = new AlertRuleEmailNotification
        {
            DateCreated = DateTimeOffset.UtcNow,
            AlertRuleId = model.AlertRuleId,
            Body = model.Body,
            DeliveryStatus = model.DeliveryStatus.ToString(),
            Subject = model.Subject,
            To = model.To
        };

        dbContext.AlertRuleEmailNotifications.Add(dbModel);

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    public async Task<AlertRuleEmailNotificationReturnModel> UpdateDeliveryStatusAsync(int id,
        NotificationDeliveryStatus deliveryStatus, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.AlertRuleEmailNotifications
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken).ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Rule notification with id {id} not found");
        }

        dbModel.DeliveryStatus = deliveryStatus.ToString();

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    public async Task<AlertRuleEmailNotificationReturnModel?> GetLatestForAlertAsync(int alertId,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var notification = await dbContext.AlertRuleEmailNotifications.Where(a => a.AlertRuleId == alertId)
            .OrderByDescending(a => a.DateCreated).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        return notification == null ? null : ToReturnModel(notification);
    }

    private static AlertRuleEmailNotificationReturnModel ToReturnModel(AlertRuleEmailNotification model)
    {
        return new AlertRuleEmailNotificationReturnModel
        {
            DateCreated = model.DateCreated,
            Id = model.Id,
            Body = model.Body,
            AlertRuleId = model.AlertRuleId,
            To = model.To,
            DeliveryStatus = model.DeliveryStatus,
            Subject = model.Subject
        };
    }
}