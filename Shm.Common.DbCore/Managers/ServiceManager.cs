using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Services;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class ServiceManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : IServiceManager
{
    public async Task<CommonListModel<ServiceReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var query = dbContext.Services.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }

        var results = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new CommonListModel<ServiceReturnModel>
        {
            TotalCount = results.Count,
            Items = results.Select(ToReturnModel).ToList(),
            Count = results.Count
        };
    }

    public async Task<ServiceReturnModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var retVal = await dbContext.Services.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        return retVal == null ? null : ToReturnModel(retVal);
    }

    /// <summary>
    /// Handles the ping status of a Service asynchronously.
    /// </summary>
    /// <param name="id">The ID of the Service to change the failed state.</param>
    /// <param name="isPingSuccessful">Is ping successful</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<ServiceReturnModel> HandlePingStatusAsync(int id, bool isPingSuccessful,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.Services
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken).ConfigureAwait(false);

        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Service with id {id} not found");
        }

        if (dbModel.IsDeleted || !dbModel.IsActive)
        {
            return ToReturnModel(dbModel);
        }

        dbModel.IsFailedState = !isPingSuccessful;
        if (isPingSuccessful)
        {
            dbModel.LastSuccessTime = DateTimeOffset.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    private static ServiceReturnModel ToReturnModel(Service model)
    {
        return new ServiceReturnModel
        {
            IsDeleted = model.IsDeleted,
            Id = model.Id,
            PingUrl = model.PingUrl,
            NotifiedForFailure = model.NotifiedForFailure,
            IsFailedState = model.IsFailedState,
            LastSuccessTime = model.LastSuccessTime,
            IsActive = model.IsActive,
            IsWorker = model.IsWorker,
            ServiceName = model.ServiceName
        };
    }
}