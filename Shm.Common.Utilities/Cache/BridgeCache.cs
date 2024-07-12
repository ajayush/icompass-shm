using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Bridges;

namespace BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;

public class BridgeCache
{
    public void Refresh(IList<BridgeReturnModel> bridgeItems)
    {
        var dict = new Dictionary<int, BridgeReturnModel>();
        foreach (var item in bridgeItems)
        {
            dict[item.Id] = item;
        }

        _bridgeCache = dict;
    }

    public BridgeReturnModel? GetById(int id)
    {
        if (_bridgeCache.TryGetValue(id, out var bridge))
        {
            return bridge;
        }

        return null;
    }

    public CommonListModel<BridgeReturnModel> Get(bool includeDeleted)
    {
        var query = _bridgeCache.Values.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }
        var retVal = new CommonListModel<BridgeReturnModel>
        {
            Items = query.OrderBy(a => a.Id)
                .ToList()
        };

        retVal.TotalCount = retVal.Items.Count;
        retVal.Count = retVal.Items.Count;
        return retVal;
    }


    public void AddOrUpdate(BridgeReturnModel model)
    {
        if (_bridgeCache.ContainsKey(model.Id))
        {
            _bridgeCache[model.Id] = model;
        }
        else
        {
            _bridgeCache.Add(model.Id, model);
        }
    }

    public void Delete(int id)
    {
        if (_bridgeCache.TryGetValue(id, out var bridge))
        {
            bridge.IsDeleted = true;
        }
    }

    private Dictionary<int, BridgeReturnModel> _bridgeCache = new();
}