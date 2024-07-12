using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.EmailNotification;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class AlertRuleManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : IAlertRuleManager
{
    /// <summary>
    /// Retrieves a list of alert rules.
    /// </summary>
    /// <param name="includeDeleted">Flag indicating whether to include deleted alert rules.</param>
    /// <param name="includeInactive">Flag indicating whether to include inactive alert rules.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="AlertRuleReturnModel"/>.</returns>
    public async Task<CommonListModel<AlertRuleReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var query = dbContext.AlertRules.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }

        var results = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new CommonListModel<AlertRuleReturnModel>
        {
            TotalCount = results.Count,
            Items = results.Select(ToReturnModel).ToList(),
            Count = results.Count
        };
    }

    /// <summary>
    /// Retrieves an alert rule by its ID.
    /// </summary>
    /// <param name="id">The ID of the alert rule.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="AlertRuleReturnModel"/>.</returns>
    public async Task<AlertRuleReturnModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.AlertRules.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);

        return dbModel == null ? null : ToReturnModel(dbModel);
    }

    /// <summary>
    /// Creates a new alert rule.
    /// </summary>
    /// <param name="model">The alert rule create model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="AlertRuleReturnModel"/>.</returns>
    public async Task<AlertRuleReturnModel> CreateAsync(AlertRuleCreateModel model, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = new AlertRule
        {
            ExpressionString = model.ExpressionString,
            IsFailedState = false,
            ToEmailAddresses = model.ToEmailAddresses,
            Description = model.Description,
            RuleName = model.RuleName,
            SensorOutputId = model.SensorOutputId,
            AlertType = model.AlertType.ToString(),
            IsActive = true
        };

        dbModel.AddCreateModelProperties(perRequestContext);
        dbContext.AlertRules.Add(dbModel);

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    /// <summary>
    /// Updates an existing alert rule.
    /// </summary>
    /// <param name="id">The ID of the alert rule to update.</param>
    /// <param name="model">The alert rule update model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="AlertRuleReturnModel"/>.</returns>
    public async Task<AlertRuleReturnModel> UpdateAsync(int id, AlertRuleUpdateModel model, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.AlertRules.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Rule with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Rule with id {id} has been deleted");
        }

        dbModel.Description = model.Description;
        dbModel.RuleName = model.RuleName;
        dbModel.ItemLastModifiedById = perRequestContext.UserContext.UserId;
        dbModel.DateLastModified = DateTimeOffset.UtcNow;
        dbModel.ToEmailAddresses = model.ToEmailAddresses;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    /// <summary>
    /// Deletes an alert rule by its ID.
    /// </summary>
    /// <param name="id">The ID of the alert rule to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.AlertRules.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Rule with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            return false;
        }

        dbModel.IsDeleted = true;
        dbModel.ItemLastModifiedById = perRequestContext.UserContext.UserId;
        dbModel.DateLastModified = DateTimeOffset.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    /// Changes the active status of an alert rule.
    /// </summary>
    /// <param name="id">The ID of the alert rule to update.</param>
    /// <param name="isActive">Flag indicating whether the alert rule should be active.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the update was successful.</returns>
    public async Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var dbModel = await dbContext.AlertRules.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} is deleted");
        }

        dbModel.IsActive = isActive;
        dbModel.ItemLastModifiedById = perRequestContext.UserContext.UserId;
        dbModel.DateLastModified = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    /// <summary>
    /// Changes the failed state of an alert rule.
    /// </summary>
    /// <param name="id">The ID of the alert rule to update.</param>
    /// <param name="failedState">Flag indicating whether the alert rule is in a failed state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="AlertRuleReturnModel"/>.</returns>
    public async Task<AlertRuleReturnModel> ChangeFailedStateAsync(int id, bool failedState, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.AlertRules
            .Include(a => a.LastAlertRuleFailure)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken).ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Alert Rule with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            return ToReturnModel(dbModel);
        }

        if (dbModel.IsFailedState == failedState)
        {
            return ToReturnModel(dbModel);
        }

        if (failedState)
        {
            var alertRuleFailure = new AlertRuleFailure
            {
                AlertRuleId = dbModel.Id,
                StartTime = DateTimeOffset.UtcNow,
            };

            dbModel.LastAlertRuleFailure = alertRuleFailure;
            dbContext.AlertRuleFailures.Add(alertRuleFailure);
            alertRuleFailure.ToEmailAddresses = dbModel.ToEmailAddresses;
        }
        else if (dbModel.LastAlertRuleFailure != null)
        {
            dbModel.LastAlertRuleFailure.EndTime = DateTimeOffset.UtcNow;
            dbModel.LastAlertRuleFailure = null;
        }

        dbModel.IsFailedState = failedState;
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    /// <summary>
    /// Retrieves a list of alert rule failures.
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of failures matching the query</returns>
    public async Task<CommonListModel<AlertRuleFailureReturnModel>> GetFailuresAsync(
        GridQueryModel<AlertRuleFailureQueryModel> query, CancellationToken cancellationToken)
    {
        var alertRuleFailures = dbContext.AlertRuleFailures.AsQueryable();
        query.CustomFilters ??= new AlertRuleFailureQueryModel();
        if (query.CustomFilters.AlertRuleId.HasValue)
        {
            alertRuleFailures = alertRuleFailures.Where(a => a.AlertRuleId == query.CustomFilters.AlertRuleId);
        }

        if (query.CustomFilters.StartTime.HasValue)
        {
            alertRuleFailures = alertRuleFailures.Where(a => a.StartTime >= query.CustomFilters.StartTime);
        }

        if (query.CustomFilters.EndTime.HasValue)
        {
            alertRuleFailures = alertRuleFailures.Where(a => a.EndTime <= query.CustomFilters.EndTime);
        }

        if (query.CustomFilters.SensorId.HasValue)
        {
            alertRuleFailures = alertRuleFailures.Where(a => a.AlertRule.SensorOutput.SensorId == query.CustomFilters.SensorId);
        }

        if (query.CustomFilters.DaqId.HasValue)
        {
            alertRuleFailures = alertRuleFailures.Where(a => a.AlertRule.SensorOutput.Sensor.DaqId == query.CustomFilters.DaqId);
        }

        if (query.CustomFilters.BridgeId.HasValue)
        {
            alertRuleFailures = alertRuleFailures.Where(a => a.AlertRule.SensorOutput.Sensor.Daq.BridgeId == query.CustomFilters.BridgeId);
        }

        if (query.CustomFilters.IsNotificationSentForFailure.HasValue)
        {
            alertRuleFailures = alertRuleFailures.Where(a => a.IsNotificationSentForFailure == query.CustomFilters.IsNotificationSentForFailure);
        }

        var count = await alertRuleFailures.CountAsync(cancellationToken).ConfigureAwait(false);
        var results = await alertRuleFailures.OrderBy(a=>a.Id).Skip(query.Skip).Take(query.Take).ToListAsync(cancellationToken).ConfigureAwait(false);
        return new CommonListModel<AlertRuleFailureReturnModel>()
        {
            TotalCount = count,
            Items = results.Select(ToReturnModel).ToList(),
            Count = results.Count
        };
    }

    /// <summary>
    /// Mark the items as completed with notification sent
    /// </summary>
    /// <param name="failedStates">Failed items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task MarkNotifiedForFailureAsync(IList<int> failedStates, CancellationToken cancellationToken)
    {
        var alertRuleFailures = await dbContext.AlertRuleFailures.Where(a => failedStates.Contains(a.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
        foreach (var alertRuleFailure in alertRuleFailures)
        {
            alertRuleFailure.IsNotificationSentForFailure = true;
        }

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private static AlertRuleReturnModel ToReturnModel(AlertRule dbModel)
    {
        var retVal = new AlertRuleReturnModel
        {
            Description = dbModel.Description,
            ExpressionString = dbModel.ExpressionString,
            Id = dbModel.Id,
            IsFailedState = dbModel.IsFailedState,
            RuleName = dbModel.RuleName,
            SensorOutputId = dbModel.SensorOutputId,
            ToEmailAddresses = dbModel.ToEmailAddresses,
            AlertType = dbModel.AlertType,
            IsActive = dbModel.IsActive
        };

        retVal.AddReturnModelProperties(dbModel);
        return retVal;
    }

    private static AlertRuleFailureReturnModel ToReturnModel(AlertRuleFailure dbModel)
    {
        return new AlertRuleFailureReturnModel
        {
            Id = dbModel.Id,
            AlertRuleId = dbModel.AlertRuleId,
            StartTime = dbModel.StartTime,
            EndTime = dbModel.EndTime,
            IsNotificationSentForFailure = dbModel.IsNotificationSentForFailure,
            IsNotificationSentForResolved = dbModel.IsNotificationSentForResolved
        };
    }
}