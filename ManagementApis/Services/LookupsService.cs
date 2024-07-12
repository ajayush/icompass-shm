using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class LookupsService(ILookupsManager lookupsManager, IBridgeManager bridgeManager)
{
    public Task<CommonListModel<SelectListItem>> GetSpanOrPierAsync(CancellationToken cancellationToken)
    {
        return lookupsManager.GetSpanOrPierAsync(cancellationToken);
    }

    public Task<CommonListModel<SelectListItem>> GetUsDsAsync(CancellationToken cancellationToken)
    {
        return lookupsManager.GetUsDsAsync(cancellationToken);
    }

    public async Task<CommonListModel<SelectListItem>> GetBridgesAsync(CancellationToken cancellationToken)
    {
        var bridges = await bridgeManager.GetAsync(false, cancellationToken).ConfigureAwait(false);
        return new CommonListModel<SelectListItem>
        {
            Items = bridges.Items.Select(a => new SelectListItem
            {
                Text = a.BridgeName,
                Value = a.Id.ToString()
            }).ToList(),
            TotalCount = bridges.TotalCount,
            Count = bridges.Count
        };
    }

    public Task<CommonListModel<SelectListItem>> GetSystemAsync(CancellationToken cancellationToken)
    {
        return lookupsManager.GetSystemAsync(cancellationToken);
    }
}