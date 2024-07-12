namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;

public class WorkerIterationConfigModel
{
    /// <summary>
    /// Default take for each iteration
    /// </summary>
    public int DefaultTake { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int MaxCycleCountInEachLoop { get; set; }

    /// <summary>
    /// Refresh interval in seconds to fetch metadata
    /// </summary>
    public int MetaRefreshIntervalInSeconds { get; set; }

    /// <summary>
    /// Cycle interval in seconds to fetch data
    /// </summary>
    public int DataParseIntervalInSeconds { get; set; }

    /// <summary>
    /// Max number of sensors to parse
    /// </summary>
    public int MaxSensorsToParse { get; set; }

    /// <summary>
    /// Start time for the sensor if starting to parse for the first time
    /// </summary>
    public DateTime? InitialSensorParseStartTime { get; set; } = DateTime.UtcNow.AddYears(-8);
}