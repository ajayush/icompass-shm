using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;

public interface IMetadataCache
{
    IList<SensorColumnMetadataModel> GetListOfColumns();

    void Refresh(IList<SensorOutputReturnModel> sensorOutputs);
}