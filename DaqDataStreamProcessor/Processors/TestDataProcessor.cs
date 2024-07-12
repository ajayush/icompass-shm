using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;
using BridgeIntelligence.iCompass.Shm.Common.EventLogger;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;

public class TestDataProcessor(IDataEventLogger<BatchDataModel> eventSender) : IDataProcessor
{
    public async Task ParseDataAsync(IList<SensorColumnMetadataModel> columns, CancellationToken cancellationToken)
    {
        var batchDataModel = new BatchDataModel
        {
            DataList = columns.Select(column => new BatchSingleColumnDataListModel
            {
                DataName = $"{column.TableName}.{column.ColumnName}",
                TableName = column.TableName,
                ColumnName = column.ColumnName
            }).ToList()
        };

        var time = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(IterationIntervalInSeconds));
        var perEventInterval = NumberOfPointsInEachIteration / IterationIntervalInSeconds;
        var random = new Random();
        for (var valueIndex = 0; valueIndex < NumberOfPointsInEachIteration; valueIndex++)
        {
            batchDataModel.Timestamps.Add(time.AddSeconds(perEventInterval * valueIndex));
            batchDataModel.RecNumbers.Add(valueIndex);

            for (var columnIndex = 0; columnIndex < columns.Count; columnIndex++)
            {
                batchDataModel.DataList[columnIndex].Values.Add((float)(random.NextDouble() * 100));
            }
        }

        await eventSender.SendMessageAsync(batchDataModel, cancellationToken).ConfigureAwait(false);
    }

    private const int NumberOfPointsInEachIteration = 100;
    private const int IterationIntervalInSeconds = 300;
}