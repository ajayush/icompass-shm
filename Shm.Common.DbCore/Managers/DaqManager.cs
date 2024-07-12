using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Daqs;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

/// <summary>
/// Manages the operations related to DAQs (Data Acquisition)
/// </summary>
public class DaqManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : IDaqManager
{
    /// <summary>
    /// Creates a new DAQ asynchronously.
    /// </summary>
    /// <param name="model">The DAQ create model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created DAQ.</returns>
    public async Task<DaqReturnModel> CreateAsync(DaqCreateModel model, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = new Daq
        {
            BridgeId = model.BridgeId,
            IsActive = true,
            Model = model.Model,
            Name = model.Name,
            PingUrl = model.PingUrl,
            UpstreamDownstream = model.UpstreamDownstream,
            Location = model.Location,
            SpanOrPier = model.SpanOrPier
        };
        dbModel.AddCreateModelProperties(perRequestContext);
        dbContext.Daqs.Add(dbModel);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    /// <summary>
    /// Updates an existing DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to update.</param>
    /// <param name="model">The DAQ update model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated DAQ.</returns>
    public async Task<DaqReturnModel> UpdateAsync(int id, DaqUpdateModel model, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.Daqs.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);

        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} is deleted");
        }

        dbModel.Name = model.Name;
        dbModel.UpstreamDownstream = model.UpstreamDownstream;
        dbModel.Location = model.Location;
        dbModel.SpanOrPier = model.SpanOrPier;
        dbModel.Model = model.Model;
        dbModel.PingUrl = model.PingUrl;

        dbModel.AddUpdateModelProperties(perRequestContext);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    /// <summary>
    /// Gets the list of DAQs asynchronously.
    /// </summary>
    /// <param name="includeDeleted">A flag indicating whether to include deleted DAQs.</param>
    /// <param name="includeInactive">A flag indicating whether to include inactive DAQs.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of DAQs.</returns>
    public async Task<CommonListModel<DaqReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var query = dbContext.Daqs.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }

        var results = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new CommonListModel<DaqReturnModel>
        {
            TotalCount = results.Count,
            Items = results.Select(ToReturnModel).ToList(),
            Count = results.Count
        };
    }

    /// <summary>
    /// Gets a DAQ by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The retrieved DAQ, or null if not found.</returns>
    public async Task<DaqReturnModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var dbModel = await dbContext.Daqs.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        return dbModel == null ? null : ToReturnModel(dbModel);
    }

    /// <summary>
    /// Deletes a DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the DAQ was deleted, false if the DAQ was already deleted.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var dbModel = await dbContext.Daqs.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            return false;
        }

        dbModel.IsDeleted = true;
        dbModel.AddUpdateModelProperties(perRequestContext);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    /// <summary>
    /// Changes the active status of a DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to change the active status.</param>
    /// <param name="isActive">The new active status.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the active status was changed successfully.</returns>
    public async Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var dbModel = await dbContext.Daqs.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} is deleted");
        }

        dbModel.IsActive = isActive;
        dbModel.AddUpdateModelProperties(perRequestContext);

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    /// <summary>
    /// Handles the ping status of a DAQ asynchronously.
    /// </summary>
    /// <param name="id">The ID of the DAQ to change the failed state.</param>
    /// <param name="isPingSuccessful">Is ping successful</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<DaqReturnModel> HandlePingStatusAsync(int id, bool isPingSuccessful,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = await dbContext.Daqs
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken).ConfigureAwait(false);

        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} not found");
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

    private static DaqReturnModel ToReturnModel(Daq dbModel)
    {
        var retVal = new DaqReturnModel
        {
            Name = dbModel.Name,
            BridgeId = dbModel.BridgeId,
            Id = dbModel.Id,
            Model = dbModel.Model,
            UpstreamDownstream = dbModel.UpstreamDownstream,
            Location = dbModel.Location,
            SpanOrPier = dbModel.SpanOrPier,
            IsActive = dbModel.IsActive,
            PingUrl = dbModel.PingUrl,
            NotifiedForFailure = dbModel.NotifiedForFailure,
            LastSuccessTime = dbModel.LastSuccessTime,
            IsFailedState = dbModel.IsFailedState
        };

        retVal.AddReturnModelProperties(dbModel);
        return retVal;
    }
}
