using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Sensors;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class SensorService(ISensorManager manager)
{
    public Task<CommonListModel<SensorReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        return manager.GetAsync(includeDeleted, includeInactive, cancellationToken);
    }

    public Task<SensorReturnModel> CreateAsync(SensorCreateModel model,
        CancellationToken cancellationToken)
    {
        return manager.CreateAsync(model, cancellationToken);
    }

    public Task<SensorReturnModel> UpdateAsync(int id, SensorUpdateModel model,
        CancellationToken cancellationToken)
    {
        return manager.UpdateAsync(id, model, cancellationToken);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return manager.DeleteAsync(id, cancellationToken);
    }

    public Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken)
    {
        return manager.ChangeActiveStatusAsync(id, isActive, cancellationToken);
    }
}