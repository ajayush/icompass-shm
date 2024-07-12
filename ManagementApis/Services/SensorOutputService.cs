using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class SensorOutputService(PerRequestContext requestContext, ISensorOutputManager manager)
{
    public Task<CommonListModel<SensorOutputReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        return manager.GetAsync(includeDeleted, includeInactive, cancellationToken);
    }

    public Task<SensorOutputReturnModel> CreateAsync(SensorOutputCreateModel model,
        CancellationToken cancellationToken)
    {
        return manager.CreateAsync(model, cancellationToken);
    }

    public Task<SensorOutputReturnModel> UpdateAsync(int id, SensorOutputUpdateModel model,
        CancellationToken cancellationToken)
    {
        return manager.UpdateAsync(id, model, cancellationToken);
    }

    public async Task<SensorOutputReturnModel> ChangeFailedStateAsync(int id, bool failedState,
        CancellationToken cancellationToken)
    {
        var sensorOutput = await manager.GetAsync(id, cancellationToken).ConfigureAwait(failedState);

        // Alert does not exist or is deleted
        if (sensorOutput == null || sensorOutput.IsDeleted)
        {
            requestContext.Logger.LogInformation("Sensor Output does not exist or has been deleted");
            throw ExceptionHelper.ItemNotFoundException($"Sensor Output with id {id} not found");
        }

        // Alert is inactive
        if (!sensorOutput.IsActive)
        {
            requestContext.Logger.LogInformation("Sensor Output is not in active state");
            return sensorOutput;
        }

        // Alert is already in the desired state.
        if (sensorOutput.IsFailedState == failedState)
        {
            requestContext.Logger.LogInformation("Sensor Output is already in desired state");
            return sensorOutput;
        }

        return await manager.ChangeFailedStateAsync(id, failedState, cancellationToken)
            .ConfigureAwait(failedState);
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