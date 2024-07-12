using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;

public interface IDataProcessor
{
    /// <summary>
    /// Method to iteratively parse data
    /// </summary>
    /// <param name="columns">Column list</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task ParseDataAsync(IList<SensorColumnMetadataModel> columns, CancellationToken cancellationToken);
}