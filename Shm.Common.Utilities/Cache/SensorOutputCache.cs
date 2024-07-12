using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;

namespace BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;

public class SensorOutputCache
{
    public void Refresh(IList<SensorOutputReturnModel> sensorOutputItems)
    {
        _sensorOutputIdCache = sensorOutputItems.ToDictionary(a => a.Id, a => a);
        _sensorOutputTableCache = sensorOutputItems.ToDictionary(a => $"{a.TableName}:{a.ColumnName}", a => a);
    }

    public SensorOutputReturnModel? GetWithId(int id)
    {
        if (_sensorOutputIdCache.TryGetValue(id, out var sensorOutput))
        {
            return sensorOutput;
        }

        return null;
    }

    public CommonListModel<SensorOutputReturnModel> Get(bool includeDeleted, bool includeInactive)
    {
        var query = _sensorOutputIdCache.Values.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }
        var retVal = new CommonListModel<SensorOutputReturnModel>
        {
            Items = query.OrderBy(a => a.Id)
                .ToList()
        };
        retVal.TotalCount = retVal.Items.Count;
        retVal.Count = retVal.Items.Count;
        return retVal;
    }

    public SensorOutputReturnModel? GetByTableAndColumn(string tableName, string columnName)
    {
        return GetByDataName($"{tableName}:{columnName}");
    }

    public SensorOutputReturnModel? GetByDataName(string dataName)
    {
        if (_sensorOutputTableCache.TryGetValue(dataName, out var sensorOutput))
        {
            return sensorOutput;
        }

        return null;
    }

    public void AddOrUpdate(SensorOutputReturnModel model)
    {
        if (_sensorOutputIdCache.ContainsKey(model.Id))
        {
            _sensorOutputIdCache[model.Id] = model;
        }
        else
        {
            _sensorOutputIdCache.Add(model.Id, model);
        }
    }

    public void Delete(int id)
    {
        if (_sensorOutputIdCache.TryGetValue(id, out var sensorOutput))
        {
            sensorOutput.IsDeleted = true;
        }
    }

    public void UpdateActiveStatus(int id, bool isActive)
    {
        if (_sensorOutputIdCache.TryGetValue(id, out var sensorOutput))
        {
            sensorOutput.IsActive = isActive;
        }
    }

    private Dictionary<int, SensorOutputReturnModel> _sensorOutputIdCache = new();
    private Dictionary<string, SensorOutputReturnModel> _sensorOutputTableCache = new();
}