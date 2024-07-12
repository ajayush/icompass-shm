using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface ISensorOutputManager
{
    Task<SensorOutputReturnModel> CreateAsync(SensorOutputCreateModel model,
        CancellationToken cancellationToken);

    Task<SensorOutputReturnModel> UpdateAsync(int id, SensorOutputUpdateModel model,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken);

    Task<CommonListModel<SensorOutputReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken);

    Task<SensorOutputReturnModel> ChangeFailedStateAsync(int id, bool failedState,
        CancellationToken cancellationToken);

    Task<SensorOutputReturnModel?> GetAsync(int id, CancellationToken cancellationToken);
}