using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputSummary;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class SensorOutputSummaryManager(PerRequestContext perRequestContext, ShmDbContext dbContext)
{
    public async Task<SensorOutputSummaryQueryReturnModel> CreateAsync(SensorOutputSummaryCreateModel model, CancellationToken cancellationToken)
    {
        var dbModel = new SensorOutputSummary
        {
            Minimum = model.Minimum,
            Maximum = model.Maximum,
            Median = model.Median,
            StandardDev = model.StandardDev,
            FirstQuartile = model.FirstQuartile,
            ThirdQuartile = model.ThirdQuartile,
            SensorOutputId = model.SensorOutputId,
            DateCreated = model.DateCreated,
            BridgeId = model.BridgeId,
            Average = model.Average,
            Id = Guid.NewGuid().ToString()
        };

        dbContext.SensorOutputSummary.Add(dbModel);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return ToReturnModel(dbModel);
    }

    public async Task<CommonListModel<SensorOutputSummaryQueryReturnModel>> QueryAsync(GridQueryModel<SensorOutputSummaryQueryModel> model, CancellationToken cancellationToken)
    {
        var query = dbContext.SensorOutputSummary.AsQueryable();
        model.CustomFilters ??= new SensorOutputSummaryQueryModel();

        if (model.CustomFilters.BridgeId.HasValue)
        {
            query = query.Where(a => a.BridgeId == model.CustomFilters.BridgeId);
        }

        if (model.CustomFilters.SensorOutputId.HasValue)
        {
            query = query.Where(a => a.SensorOutputId == model.CustomFilters.SensorOutputId);
        }

        if (model.CustomFilters.StartDate.HasValue)
        {
            query = query.Where(a => a.DateCreated >= model.CustomFilters.StartDate);
        }

        if (model.CustomFilters.EndDate.HasValue)
        {
            query = query.Where(a => a.DateCreated <= model.CustomFilters.EndDate);
        }

        var totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);
        query = query.OrderByDescending(a => a.DateCreated).Skip(model.Skip).Take(model.Take);
        
        var results = await query.Skip(model.Skip).Take(model.Take).ToListAsync(cancellationToken).ConfigureAwait(false);
        return new CommonListModel<SensorOutputSummaryQueryReturnModel>()
        {
            Count = results.Count,
            Items = results.Select(ToReturnModel).ToList(),
            TotalCount = totalCount
        };
    }

    private SensorOutputSummaryQueryReturnModel ToReturnModel(SensorOutputSummary model)
    {
        return new SensorOutputSummaryQueryReturnModel
        {
            Id = model.Id,
            BridgeId = model.BridgeId,
            SensorOutputId = model.SensorOutputId,
            Minimum = model.Minimum,
            Maximum = model.Maximum,
            Average = model.Average,
            Median = model.Median,
            StandardDev = model.StandardDev,
            SensorOutputName = perRequestContext.CacheFactory.SensorCache.GetWithId(perRequestContext.CacheFactory.SensorOutputCache.GetWithId(model.SensorOutputId)?.Id ?? 0)?.Name ?? "",
            BridgeName = perRequestContext.CacheFactory.BridgeCache.GetById(model.BridgeId)?.BridgeName ?? "",
            FirstQuartile = model.FirstQuartile,
            ThirdQuartile = model.ThirdQuartile,
            DateCreated = model.DateCreated
        };
    }
}