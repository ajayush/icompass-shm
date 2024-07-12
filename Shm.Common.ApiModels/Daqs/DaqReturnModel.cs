using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Daqs;

public class DaqReturnModel : CommonReturnModel
{
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;

    public int BridgeId { get; set; }

    public string? UpstreamDownstream { get; set; }

    public string? Model { get; set; }
    
    public string? SpanOrPier { get; set; }
    
    public string? Location { get; set; }
    
    public string? PingUrl { get; set; }

    public bool IsFailedState { get; set; }

    public DateTimeOffset? LastSuccessTime { get; set; }

    public bool IsActive { get; set; }

    public bool NotifiedForFailure { get; set; }
}