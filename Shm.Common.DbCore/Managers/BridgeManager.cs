using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.CommonCore.Utilities.Exceptions;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Bridges;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

/// <summary>
/// Manages the operations related to bridges in the database.
/// </summary>
public class BridgeManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : IBridgeManager
{
    /// <summary>
    /// Creates a new bridge asynchronously.
    /// </summary>
    /// <param name="model">The bridge create model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created bridge.</returns>
    public async Task<BridgeReturnModel> CreateAsync(BridgeCreateModel model, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var bridge = new Bridge
        {
            BridgeAbbreviation = model.BridgeAbbreviation,
            BridgeName = model.BridgeName
        };

        bridge.AddCreateModelProperties(perRequestContext);
        dbContext.Bridges.Add(bridge);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ToReturnModel(bridge);
    }

    /// <summary>
    /// Gets the list of bridges asynchronously.
    /// </summary>
    /// <param name="includeDeleted">A flag indicating whether to include deleted bridges.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of bridges.</returns>
    public async Task<CommonListModel<BridgeReturnModel>> GetAsync(bool includeDeleted, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var bridges = dbContext.Bridges.AsQueryable();
        if (!includeDeleted)
        {
            bridges = bridges.Where(a => !a.IsDeleted);
        }

        var results = await bridges.ToListAsync(cancellationToken).ConfigureAwait(false);

        return new CommonListModel<BridgeReturnModel>
        {
            Count = results.Count,
            Items = results.Select(ToReturnModel).ToList(),
            TotalCount = results.Count
        };
    }

    /// <summary>
    /// Deletes a bridge asynchronously.
    /// </summary>
    /// <param name="id">The ID of the bridge to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the bridge was deleted, otherwise false.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();

        var bridge = await dbContext.Bridges.FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            .ConfigureAwait(false);

        if (bridge == null)
        {
            throw ExceptionHelper.ItemNotFoundException($"Bridge with id {id} not found");
        }

        if (bridge.IsDeleted)
        {
            return false;
        }

        bridge.IsDeleted = true;
        bridge.ItemLastModifiedById = perRequestContext.UserContext.UserId;
        bridge.DateLastModified = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    private static BridgeReturnModel ToReturnModel(Bridge bridge)
    {
        var retVal = new BridgeReturnModel
        {
            BridgeName = bridge.BridgeName,
            BridgeAbbreviation = bridge.BridgeAbbreviation,
            Id = bridge.Id
        };

        retVal.AddReturnModelProperties(bridge);

        return retVal;
    }
}