using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Sensors;

namespace BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;

public class SensorCache
{
    public void Refresh(IList<SensorReturnModel> sensorItems)
    {
        var dict = new Dictionary<int, SensorReturnModel>();
        foreach (var item in sensorItems)
        {
            dict[item.Id] = item;
        }

        _sensorCache = dict;
    }

    public CommonListModel<SensorReturnModel> Get(bool includeDeleted, bool includeInactive)
    {
        var query = _sensorCache.Values.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }

        var retVal = new CommonListModel<SensorReturnModel>
        {
            Items = query.OrderBy(a => a.Id).ToList()
        };

        retVal.TotalCount = retVal.Items.Count;
        retVal.Count = retVal.Items.Count;
        return retVal;
    }

    public SensorReturnModel? GetWithId(int id)
    {
        if (_sensorCache.TryGetValue(id, out var sensor))
        {
            return sensor;
        }

        return null;
    }

    public void Delete(int id)
    {
        if (_sensorCache.TryGetValue(id, out var sensor))
        {
            sensor.IsDeleted = true;
        }
    }

    public void AddOrUpdate(SensorReturnModel model)
    {
        if (_sensorCache.ContainsKey(model.Id))
        {
            _sensorCache[model.Id] = model;
        }
        else
        {
            _sensorCache.Add(model.Id, model);
        }
    }

    public void UpdateActiveStatus(int id, bool isActive)
    {
        if (_sensorCache.TryGetValue(id, out var sensor))
        {
            sensor.IsActive = isActive;
        }
    }

    private Dictionary<int, SensorReturnModel> _sensorCache = new();
}