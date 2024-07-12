using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Daqs;

namespace BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;

public class DaqCache
{
    public void Refresh(IList<DaqReturnModel> bridgeItems)
    {
        var dict = new Dictionary<int, DaqReturnModel>();
        foreach (var item in bridgeItems)
        {
            dict[item.Id] = item;
        }

        _daqCache = dict;
    }

    public CommonListModel<DaqReturnModel> Get(bool includeDeleted, bool includeInactive)
    {
        var query = _daqCache.Values.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }
        var retVal = new CommonListModel<DaqReturnModel>
        {
            Items = query.OrderBy(a => a.Id).ToList()
        };

        retVal.TotalCount = retVal.Items.Count;
        retVal.Count = retVal.Items.Count;
        return retVal;
    }

    public DaqReturnModel? GetWithId(int id)
    {
        if (_daqCache.TryGetValue(id, out var daq))
        {
            return daq;
        }

        return null;
    }

    public void AddOrUpdate(DaqReturnModel model)
    {
        if (_daqCache.ContainsKey(model.Id))
        {
            _daqCache[model.Id] = model;
        }
        else
            _daqCache.Add(model.Id, model);
    }

    public void UpdateActiveStatus(int id, bool isActive)
    {
        if (_daqCache.TryGetValue(id, out var daq))
        {
            daq.IsActive = isActive;
        }
    }
    public void Delete(int id)
    {
        if (_daqCache.TryGetValue(id, out var daq))
        {
            daq.IsDeleted = true;
        }
    }

    private Dictionary<int, DaqReturnModel> _daqCache = new();
}