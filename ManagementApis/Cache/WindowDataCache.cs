using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;
using BridgeIntelligence.iCompass.Shm.Management.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Cache;

public class WindowDataCache(IIxSingletonLogger logger)
{
    public void Initialize(IList<DashboardDataConfigModel> models)
    {
        logger.LogTrace();
        foreach (var model in models)
        {
            if (model.DataType == DashboardDataTypes.Point)
            {
                continue;
            }

            if (!_windowDataDict.ContainsKey($"{model.TableName}.{model.ColumnName}"))
            {
                _windowDataDict.Add($"{model.TableName}.{model.ColumnName}", new List<DashboardWindowDataModel>());
            }

            _windowDataDict[$"{model.TableName}.{model.ColumnName}"].Add(new DashboardWindowDataModel
            {
                TableName = model.TableName,
                ColumnName = model.ColumnName,
                DataName = model.DataName,
                BridgeName = model.BridgeName.ToString(),
                MaxInterval = model.MaxIntervalInSeconds.HasValue ? TimeSpan.FromSeconds(model.MaxIntervalInSeconds.Value) : null,
                MaxNumberOfPoints = model.MaxNumberOfPoints
            });
        }
    }

    public void Add(string sensorOutputName, List<float?> data, List<DateTime> time)
    {
        if (!CheckIfProcessable(sensorOutputName))
        {
            return;
        }

        foreach (var windowData in _windowDataDict[sensorOutputName])
        {
            for (var i = 0; i < data.Count; i++)
            {
                windowData.AddPoint(data[i], time[i]);
            }
        }
    }

    public List<DashboardWindowDataModel> Get()
    {
        var retVal = new List<DashboardWindowDataModel>();
        foreach (var windowDataModel in _windowDataDict.Values.SelectMany(a => a))
        {
            windowDataModel.Clean();
            retVal.Add(windowDataModel);
        }

        return retVal;
    }

    public DashboardWindowDataModel? Get(string bridgeName, string dataName)
    {
        return _windowDataDict.Values.SelectMany(a => a)
            .FirstOrDefault(a => a.BridgeName == bridgeName && a.DataName == dataName);
    }

    private bool CheckIfProcessable(string sensorOutputName)
    {
        return _windowDataDict.ContainsKey(sensorOutputName);
    }

    private readonly Dictionary<string, List<DashboardWindowDataModel>> _windowDataDict = new();
}