using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Bridges;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

/// <summary>
/// Manages the operations related to bridges in the database.
/// </summary>
public interface IBridgeManager
{
    /// <summary>
    /// Creates a new bridge asynchronously.
    /// </summary>
    /// <param name="model">The bridge create model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created bridge.</returns>
    Task<BridgeReturnModel> CreateAsync(BridgeCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the list of bridges asynchronously.
    /// </summary>
    /// <param name="includeDeleted">A flag indicating whether to include deleted bridges.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of bridges.</returns>
    Task<CommonListModel<BridgeReturnModel>> GetAsync(bool includeDeleted, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a bridge asynchronously.
    /// </summary>
    /// <param name="id">The ID of the bridge to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the bridge was deleted, otherwise false.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}