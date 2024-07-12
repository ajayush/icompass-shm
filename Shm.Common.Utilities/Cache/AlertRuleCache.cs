using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;

namespace BridgeIntelligence.iCompass.Shm.Common.Utilities.Cache;

public class AlertRuleCache
{
    public void Refresh(IList<AlertRuleReturnModel> alertItems)
    {
        _alertCache = alertItems.ToDictionary(a => a.Id, a => a);
        var dict = new Dictionary<int, IList<AlertRuleReturnModel>>();
        foreach (var item in alertItems)
        {
            if (!dict.ContainsKey(item.SensorOutputId))
            {
                dict.Add(item.SensorOutputId, new List<AlertRuleReturnModel>());
            }

            dict[item.SensorOutputId].Add(item);
        }

        _sensorAlertRuleMap = dict;
    }

    public CommonListModel<AlertRuleReturnModel> Get(bool includeDeleted, bool includeInactive)
    {
        var query = _alertCache.Values.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(a => !a.IsDeleted);
        }

        if (!includeInactive)
        {
            query = query.Where(a => a.IsActive);
        }
        var retVal = new CommonListModel<AlertRuleReturnModel>
        {
            Items = query.OrderBy(a => a.Id).ToList()
        };
        retVal.TotalCount = retVal.Items.Count;
        retVal.Count = retVal.Items.Count;
        return retVal;
    }

    public CommonListModel<AlertRuleReturnModel> GetBySensorOutputId(int sensorOutputId)
    {
        if (_sensorAlertRuleMap?.TryGetValue(sensorOutputId, out var alertRules) == true)
        {
            return new CommonListModel<AlertRuleReturnModel>
            {
                Items = alertRules,
                Count = alertRules.Count,
                TotalCount = alertRules.Count
            };
        }

        return new CommonListModel<AlertRuleReturnModel>();
    }

    public AlertRuleReturnModel? GetAlertWithId(int id)
    {
        if (_alertCache.TryGetValue(id, out var alertRule))
        {
            return alertRule;
        }

        return null;
    }

    public void AddOrUpdate(AlertRuleReturnModel model)
    {
        if (_alertCache.ContainsKey(model.Id))
        {
            _alertCache[model.Id] = model;
        }
        else
        {
            _alertCache.Add(model.Id, model);
        }
    }

    public void Delete(int id)
    {
        if (_alertCache.TryGetValue(id, out var alertRule))
        {
            alertRule.IsDeleted = true;
        }
    }

    private Dictionary<int, AlertRuleReturnModel> _alertCache = new();

    private Dictionary<int, IList<AlertRuleReturnModel>>? _sensorAlertRuleMap = new();
}