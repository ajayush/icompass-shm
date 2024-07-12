using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Bridges;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class BridgeService(IBridgeManager bridgeManager)
{
    public Task<BridgeReturnModel> CreateAsync(BridgeCreateModel model, CancellationToken cancellationToken)
    {
        return bridgeManager.CreateAsync(model, cancellationToken);
    }

    public Task<CommonListModel<BridgeReturnModel>> GetAsync(bool includeDeleted,
        CancellationToken cancellationToken)
    {
        return bridgeManager.GetAsync(includeDeleted, cancellationToken);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return bridgeManager.DeleteAsync(id, cancellationToken);
    }
}