using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class LookupsManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : ILookupsManager
{
    public async Task<CommonListModel<SelectListItem>> GetSpanOrPierAsync(CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var results = await dbContext.Sensors.Select(a => a.SpanOrPier).Distinct().ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new CommonListModel<SelectListItem>
        {
            Count = results.Count,
            TotalCount = results.Count,
            Items = results.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a!).Select(a => new SelectListItem
            {
                Text = a,
                Value = a
            }).ToList()
        };
    }

    public async Task<CommonListModel<SelectListItem>> GetUsDsAsync(CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var results1 = await dbContext.Sensors.Select(a => a.UpstreamDownstream).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var results2 = await dbContext.Daqs.Select(a => a.UpstreamDownstream).ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var retVal = new List<string>();
        retVal.AddRange(results1.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a!));
        retVal.AddRange(results2.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a!));
        var results = retVal.Distinct().ToList();

        return new CommonListModel<SelectListItem>
        {
            Count = results.Count,
            TotalCount = results.Count,
            Items = results.Select(a => new SelectListItem
            {
                Text = a,
                Value = a
            }).ToList()
        };
    }

    public async Task<CommonListModel<SelectListItem>> GetSystemAsync(CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var results = await dbContext.Sensors.Select(a => a.System).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var retVal = new List<string>();
        retVal.AddRange(results.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a!));
        var finalResults = retVal.Distinct().Select(a => new SelectListItem
        {
            Text = a,
            Value = a
        }).ToList();

        return new CommonListModel<SelectListItem>
        {
            Count = finalResults.Count,
            TotalCount = finalResults.Count,
            Items = finalResults
        };
    }
}