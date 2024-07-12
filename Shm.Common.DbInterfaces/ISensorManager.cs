using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Sensors;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface ISensorManager
{
    /// <summary>
    /// Creates a new sensor asynchronously.
    /// </summary>
    /// <param name="model">The sensor create model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created sensor.</returns>
    Task<SensorReturnModel> CreateAsync(SensorCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing sensor asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sensor to update.</param>
    /// <param name="model">The sensor update model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated sensor.</returns>
    Task<SensorReturnModel> UpdateAsync(int id, SensorUpdateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a sensor asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sensor to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the sensor was deleted, false otherwise.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of sensors asynchronously.
    /// </summary>
    /// <param name="includeDeleted">A flag indicating whether to include deleted sensors.</param>
    /// <param name="includeInactive">A flag indicating whether to include inactive sensors.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of sensors.</returns>
    Task<CommonListModel<SensorReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken);

    /// <summary>
    /// Changes the active status of a sensor asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sensor to update.</param>
    /// <param name="isActive">A flag indicating whether the sensor should be active or not.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the active status was changed, false otherwise.</returns>
    Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken);
}