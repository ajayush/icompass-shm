namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;

public class SensorGridReturnModel
{
    public int SensorOutputId { get; set; }

    public int DaqId { get; set; }

    public string DaqName { get; set; } = default!;

    public int BridgeId { get; set; }

    public string BridgeName { get; set; } = default!;

    public string? SensorOutputType { get; set; }

    public int SensorId { get; set; }

    public string SensorName { get; set; } = default!;

    public string? UpstreamDownstream { get; set; }

    public string? System { get; set; }

    public string? Model { get; set; }

    public string? Member { get; set; }

    public string? Position { get; set; }

    public string? SpanOrPier { get; set; }

    public bool IsHealthy { get; set; }
}