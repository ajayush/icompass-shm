using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Services;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class ServiceService(IServiceManager serviceManager)
{
    public Task<CommonListModel<ServiceReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        return serviceManager.GetAsync(includeDeleted, includeInactive, cancellationToken);
    }
}