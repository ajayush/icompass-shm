using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class AlertRuleService(PerRequestContext requestContext,
    IAlertRuleManager alertRuleManager)
{
    public Task<CommonListModel<AlertRuleReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        return alertRuleManager.GetAsync(includeDeleted, includeInactive, cancellationToken);
    }

    public async Task<AlertRuleReturnModel> CreateAsync(AlertRuleCreateModel model,
        CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();
        var retVal = await alertRuleManager.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        return retVal;
    }

    public async Task<AlertRuleReturnModel> UpdateAsync(int id, AlertRuleUpdateModel model,
        CancellationToken cancellationToken)
    {
        var retVal = await alertRuleManager.UpdateAsync(id, model, cancellationToken).ConfigureAwait(false);
        return retVal;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var model = await alertRuleManager.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (model == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Rule with id {id} not found");
        }

        if (model.IsDeleted)
        {
            return false;
        }

        var retVal = await alertRuleManager.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        return retVal;
    }

    public async Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken)
    {
        var model = await alertRuleManager.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (model == null || model.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Rule with id {id} not found");
        }

        if (model.IsActive == isActive)
        {
            return false;
        }

        var retVal = await alertRuleManager.ChangeActiveStatusAsync(id, isActive, cancellationToken)
            .ConfigureAwait(false);

        return retVal;
    }
}