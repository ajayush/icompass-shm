using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;
using BridgeIntelligence.iCompass.Shm.Management.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Cache;

public class PointDataCache(IIxSingletonLogger logger)
{
    public void Initialize(IList<DashboardDataConfigModel> models)
    {
        logger.LogTrace();
        foreach (var model in models)
        {
            if(model.DataType == DashboardDataTypes.Point)
            {
                _pointDataDict.Add($"{model.TableName}.{model.ColumnName}", new DashboardPointDataModel
                {
                    TableName = model.TableName,
                    ColumnName = model.ColumnName,
                    DataName = model.DataName,
                    BridgeName = model.BridgeName.ToString()
                });
            }
        }
    }

    private bool CheckIfProcessable(string sensorOutputName)
    {
        return _pointDataDict.ContainsKey(sensorOutputName);
    }

    public void Add(string sensorOutputName, List<float?> data, List<DateTime> time)
    {
        if (!CheckIfProcessable(sensorOutputName))
        {
            return;
        }

        var pointData = _pointDataDict[sensorOutputName];

        if (time.LastOrDefault() != pointData.TimeOfPoint)
        {
            pointData.PointValue = data.LastOrDefault();
            pointData.TimeOfPoint = time.LastOrDefault();
        }
    }

    public List<DashboardPointDataModel> Get()
    {
        return _pointDataDict.Values.ToList();
    }

    public DashboardPointDataModel? Get(string bridgeName, string dataName)
    {
        return _pointDataDict.Values.FirstOrDefault(a => a.BridgeName == bridgeName && a.DataName == dataName);
    }

    private readonly Dictionary<string, DashboardPointDataModel> _pointDataDict = new();
}