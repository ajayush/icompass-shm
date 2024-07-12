namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;

public class SensorDataConfigModel
{
    /// <summary>
    /// Connection string for sensor database
    /// </summary>
    public string ConnectionString { get; set; } = default!;

    /// <summary>
    /// Column name that stores timestamp
    /// </summary>
    public string TimestampColumnName { get; set; } = "TmStamp";
    
    public string RecordNumberColumnName { get; set; } = "RecNum";
}