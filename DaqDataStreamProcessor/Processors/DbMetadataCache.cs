using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;

public class DbMetadataCache : IMetadataCache
{
    public IList<SensorColumnMetadataModel> GetListOfColumns()
    {
        return _columnList;
    }

    public void Refresh(IList<SensorOutputReturnModel> sensorOutputs)
    {
        _columnList = sensorOutputs.Select(a => new SensorColumnMetadataModel
        {
            TableName = a.TableName,
            ColumnName = a.ColumnName
        }).ToList();
    }

    private List<SensorColumnMetadataModel> _columnList = new();
}