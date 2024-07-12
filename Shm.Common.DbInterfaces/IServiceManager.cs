using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Services;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface IServiceManager
{
    Task<CommonListModel<ServiceReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken);

    /// <summary>
    /// Handles the ping status of a Service asynchronously.
    /// </summary>
    /// <param name="id">The ID of the Service to change the failed state.</param>
    /// <param name="isPingSuccessful">Is ping successful</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<ServiceReturnModel> HandlePingStatusAsync(int id, bool isPingSuccessful, CancellationToken cancellationToken);

    Task<ServiceReturnModel?> GetAsync(int id, CancellationToken cancellationToken);
}