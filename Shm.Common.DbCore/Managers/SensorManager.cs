using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Sensors;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

/// <summary>
/// Manages the operations related to sensors in the database.
/// </summary>
public class SensorManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : ISensorManager
{
    /// <summary>
    /// Creates a new sensor asynchronously.
    /// </summary>
    /// <param name="model">The sensor create model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created sensor.</returns>
    public async Task<SensorReturnModel> CreateAsync(SensorCreateModel model, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var dbModel = new Sensor
        {
            Abbreviation = model.Abbreviation,
            AlternateName = model.AlternateName,
            DaqId = model.DaqId,
            IsActive = true,
            Member = model.Member,
            Model = model.Model,
            Name = model.Name,
            Position = model.Position,
            SpanOrPier = model.SpanOrPier,
            System = model.System,
            UpstreamDownstream = model.UpstreamDownstream
        };
        dbModel.AddCreateModelProperties(perRequestContext);
        dbContext.Sensors.Add(dbModel);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    /// <summary>
    /// Updates an existing sensor asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sensor to update.</param>
    /// <param name="model">The sensor update model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated sensor.</returns>
    public async Task<SensorReturnModel> UpdateAsync(int id, SensorUpdateModel model, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var dbModel = await dbContext.Sensors.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Sensor with id {id} not found");
        }

        if (dbModel.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Sensor with id {id} is deleted");
        }

        dbModel.Abbreviation = model.Abbreviation;
        dbModel.AlternateName = model.AlternateName;
        dbModel.Member = model.Member;
        dbModel.Model = model.Model;
        dbModel.Name = model.Name;
        dbModel.Position = model.Position;
        dbModel.SpanOrPier = model.SpanOrPier;
        dbModel.System = model.System;
        dbModel.UpstreamDownstream = model.UpstreamDownstream;
        dbModel.AddUpdateModelProperties(perRequestContext);

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbModel);
    }

    /// <summary>
    /// Deletes a sensor asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sensor to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the sensor was deleted, false otherwise.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var dbModel = await dbContext.Sensors.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (dbModel == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Sensor with id {id} not found");
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
    /// Retrieves a list of sensors asynchronously.
    /// </summary>
    /// <param name="includeDeleted">A flag indicating whether to include deleted sensors.</param>
    /// <param name="includeInactive">A flag indicating whether to include inactive sensors.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of sensors.</returns>
    public async Task<CommonListModel<SensorReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var query = dbContext.Sensors.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }

        var results = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new CommonListModel<SensorReturnModel>
        {
            TotalCount = results.Count,
            Items = results.Select(ToReturnModel).ToList(),
            Count = results.Count
        };
    }

    /// <summary>
    /// Changes the active status of a sensor asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sensor to update.</param>
    /// <param name="isActive">A flag indicating whether the sensor should be active or not.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the active status was changed, false otherwise.</returns>
    public async Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var dbModel = await dbContext.Sensors.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
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

    private static SensorReturnModel ToReturnModel(Sensor dbModel)
    {
        var retVal = new SensorReturnModel
        {
            IsActive = dbModel.IsActive,
            Id = dbModel.Id,
            DaqId = dbModel.DaqId,
            Abbreviation = dbModel.Abbreviation,
            AlternateName = dbModel.AlternateName,
            Member = dbModel.Member,
            Model = dbModel.Model,
            Name = dbModel.Name,
            Position = dbModel.Position,
            SpanOrPier = dbModel.SpanOrPier,
            System = dbModel.System,
            UpstreamDownstream = dbModel.UpstreamDownstream,
        };

        retVal.AddReturnModelProperties(dbModel);
        return retVal;
    }
}
