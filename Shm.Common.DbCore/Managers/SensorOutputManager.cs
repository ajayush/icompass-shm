using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class SensorOutputManager(PerRequestContext requestContext, ShmDbContext dbContext) : ISensorOutputManager
{
    public async Task<SensorOutputReturnModel> CreateAsync(SensorOutputCreateModel model,
        CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();

        var dbOutput = new SensorOutput
        {
            ColumnName = model.ColumnName,
            LowerBound = model.LowerBound,
            UpperBound = model.UpperBound,
            TableName = model.TableName,
            Unit = model.Unit,
            SensorId = model.SensorId,
            OutputType = null,
            IsActive = true,
            IsDeleted = false,
            ItemCreatedById = requestContext.UserContext.UserId,
            DateLastModified = DateTimeOffset.UtcNow,
            ItemLastModifiedById = requestContext.UserContext.UserId,
            DateCreated = DateTimeOffset.UtcNow,
        };

        dbContext.SensorOutputs.Add(dbOutput);

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(dbOutput);
    }

    public async Task<SensorOutputReturnModel> UpdateAsync(int id, SensorOutputUpdateModel model,
        CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();

        var output = await dbContext.SensorOutputs
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (output == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Sensor output with id {id} not found");
        }

        if (output.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Sensor output with id {id} is deleted");
        }

        output.UpperBound = model.UpperBound;
        output.LowerBound = model.LowerBound;
        output.Unit = model.Unit;
        output.ItemLastModifiedById = requestContext.UserContext.UserId;
        output.DateLastModified = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(output);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();

        var output = await dbContext.SensorOutputs
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (output == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Sensor output with id {id} not found");
        }

        if (output.IsDeleted)
        {
            return false;
        }

        output.IsDeleted = true;
        output.ItemLastModifiedById = requestContext.UserContext.UserId;
        output.DateLastModified = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    public async Task<bool> ChangeActiveStatusAsync(int id, bool isActive, CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();
        var daq = await dbContext.SensorOutputs.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);
        if (daq == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} not found");
        }

        if (daq.IsDeleted)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} is deleted");
        }

        daq.IsActive = isActive;
        daq.ItemLastModifiedById = requestContext.UserContext.UserId;
        daq.DateLastModified = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    public async Task<CommonListModel<SensorOutputReturnModel>> GetAsync(bool includeDeleted, bool includeInactive,
        CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();

        var query = dbContext.SensorOutputs.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }

        var results = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new CommonListModel<SensorOutputReturnModel>
        {
            TotalCount = results.Count,
            Items = results.Select(ToReturnModel).ToList(),
            Count = results.Count
        };
    }

    public async Task<SensorOutputReturnModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();

        var alert = await dbContext.SensorOutputs.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);

        return alert == null ? null : ToReturnModel(alert);
    }

    public async Task<SensorOutputReturnModel> ChangeFailedStateAsync(int id, bool failedState, CancellationToken cancellationToken)
    {
        requestContext.Logger.LogTrace();

        var alert = await dbContext.SensorOutputs
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken).ConfigureAwait(false);

        if (alert == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Daq with id {id} not found");
        }

        alert.IsFailedState = failedState;
        if (!failedState)
        {
            alert.LastSuccessTime = DateTimeOffset.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(alert);
    }

    private static SensorOutputReturnModel ToReturnModel(SensorOutput model)
    {
        return new SensorOutputReturnModel
        {
            Id = model.Id,
            ColumnName = model.ColumnName,
            LowerBound = model.LowerBound,
            SensorId = model.SensorId,
            TableName = model.TableName,
            Unit = model.Unit,
            UpperBound = model.UpperBound,
            OutputType = model.OutputType,
            IsActive = model.IsActive,
            ItemCreatedById = model.ItemCreatedById,
            DateLastModified = model.DateLastModified,
            ItemLastModifiedById = model.ItemLastModifiedById,
            DateCreated = model.DateCreated,
            IsDeleted = model.IsDeleted,
            LastSuccessTime = model.LastSuccessTime,
            IsFailedState = model.IsFailedState
        };
    }
}