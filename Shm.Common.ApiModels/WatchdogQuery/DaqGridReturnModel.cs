namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;

public class DaqGridReturnModel
{
    public int DaqId { get; set; }

    public string DaqName { get; set; } = default!;

    public int BridgeId { get; set; }

    public string BridgeName { get; set; } = default!;

    public string BridgeAbbreviation { get; set; } = default!;
    

    public string? UpstreamDownstream { get; set; }

    public string? Model { get; set; }

    public string? SpanOrPier { get; set; }

    public string? Location { get; set; }

    public bool? IsHealthy { get; set; }
}