using BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;

public class TestMetadataCache : IMetadataCache
{
    public IList<SensorColumnMetadataModel> GetListOfColumns()
    {
        return new List<SensorColumnMetadataModel>
        {
            new()
            {
                TableName = "Table1",
                ColumnName = "Column1"
            },
            new()
            {
                TableName = "WindSpeed",
                ColumnName = "BB"
            },
            new()
            {
                TableName = "AirGap",
                ColumnName = "BB"
            },
            new()
            {
                TableName = "PAApproach",
                ColumnName = "BB"
            },
            new()
            {
                TableName = "AmbientAir",
                ColumnName = "BB"
            },
            new()
            {
                TableName = "NJApproach",
                ColumnName = "BB"
            },
            new()
            {
                TableName = "NJApproach",
                ColumnName = "Tacony"
            },
            new()
            {
                TableName = "AmbientAir",
                ColumnName = "Tacony"
            },
            new()
            {
                TableName = "PAApproach",
                ColumnName = "Tacony"
            },
            new()
            {
                TableName = "AirGap",
                ColumnName = "Tacony"
            },
            new()
            {
                TableName = "WindSpeed",
                ColumnName = "Tacony"
            },
            new()
            {
                TableName = "AirTemp",
                ColumnName = "Tacony"
            },
            new()
            {
                TableName = "NJApproach",
                ColumnName = "Riverside"
            },
            new()
            {
                TableName = "AmbientAir",
                ColumnName = "Riverside"
            },
            new()
            {
                TableName = "PAApproach",
                ColumnName = "Riverside"
            },
            new()
            {
                TableName = "AirGap",
                ColumnName = "Riverside"
            },
            new()
            {
                TableName = "WindSpeed",
                ColumnName = "Riverside"
            },
            new()
            {
                TableName = "AirTemp",
                ColumnName = "Riverside"
            }
        };
    }

    public void Refresh(IList<SensorOutputReturnModel> sensorOutputs)
    {
    }
}