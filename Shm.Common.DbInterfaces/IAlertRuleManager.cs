using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.EmailNotification;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface IAlertRuleManager
{
    /// <summary>
    /// Retrieves a list of alert rules.
    /// </summary>
    /// <param name="includeDeleted">Flag indicating whether to include deleted alert rules.</param>
    /// <param name="includeInactive">Flag indicating whether to include inactive alert rules.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="AlertRuleReturnModel"/>.</returns>
    Task<CommonListModel<AlertRuleReturnModel>> GetAsync(bool includeDeleted, bool includeInactive, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an alert rule by its ID.
    /// </summary>
    /// <param name="id">The ID of the alert rule.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="AlertRuleReturnModel"/>.</returns>
    Task<AlertRuleReturnModel?> GetAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new alert rule.
    /// </summary>
    /// <param name="model">The alert rule create model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="AlertRuleReturnModel"/>.</returns>
    Task<AlertRuleReturnModel> CreateAsync(AlertRuleCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing alert rule.
    /// </summary>
    /// <param name="id">The ID of the alert rule to update.</param>
    /// <param name="model">The alert rule update model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="AlertRuleReturnModel"/>.</returns>
    Task<AlertRuleReturnModel> UpdateAsync(int id, AlertRuleUpdateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an alert rule by its ID.
    /// </summary>
    /// <param name="id">The ID of the alert rule to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the active status of an alert rule.
    /// </summary>
    /// <param name="id">The ID of the alert rule to update.</param>
    /// <param name="isActive">Flag indicating whether the alert rule should be active.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the update was successful.</returns>
    Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the failed state of an alert rule.
    /// </summary>
    /// <param name="id">The ID of the alert rule to update.</param>
    /// <param name="failedState">Flag indicating whether the alert rule is in a failed state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="AlertRuleReturnModel"/>.</returns>
    Task<AlertRuleReturnModel> ChangeFailedStateAsync(int id, bool failedState, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of alert rule failures.
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of failures matching the query</returns>
    Task<CommonListModel<AlertRuleFailureReturnModel>> GetFailuresAsync(GridQueryModel<AlertRuleFailureQueryModel> query, CancellationToken cancellationToken);

    /// <summary>
    /// Mark the items as completed with notification sent
    /// </summary>
    /// <param name="failedStates">Failed items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task MarkNotifiedForFailureAsync(IList<int> failedStates, CancellationToken cancellationToken);
}