using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Daqs;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

/// <summary>
/// Manages the operations related to DAQs (Data Acquisition)
/// </summary>
public interface IDaqManager
{
    /// <summary>
    /// Creates a new DAQ asynchronously.
    /// </summary>
    /// <param name="model">The DAQ create model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created DAQ.</returns>
    Task<DaqReturnModel> CreateAsync(DaqCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to update.</param>
    /// <param name="model">The DAQ update model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated DAQ.</returns>
    Task<DaqReturnModel> UpdateAsync(int id, DaqUpdateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the list of DAQs asynchronously.
    /// </summary>
    /// <param name="includeDeleted">A flag indicating whether to include deleted DAQs.</param>
    /// <param name="includeInactive">A flag indicating whether to include inactive DAQs.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of DAQs.</returns>
    Task<CommonListModel<DaqReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the DAQ was deleted, false if the DAQ was already deleted.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the active status of a DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to change the active status.</param>
    /// <param name="isActive">The new active status.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the active status was changed successfully.</returns>
    Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken);

    /// <summary>
    /// Handles the ping status of a DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to change the failed state.</param>
    /// <param name="isPingSuccessful">Is ping successful</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<DaqReturnModel> HandlePingStatusAsync(int id, bool isPingSuccessful, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a DAQ by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The retrieved DAQ, or null if not found.</returns>
    Task<DaqReturnModel?> GetAsync(int id, CancellationToken cancellationToken);
}