using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Daqs;

public class DaqUpdateModel
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;

    [MaxLength(64)]
    public string? UpstreamDownstream { get; set; }

    [MaxLength(64)]
    public string? SpanOrPier { get; set; }

    [MaxLength(64)]
    public string? Location { get; set; }

    [MaxLength(64)]
    public string? Model { get; set; }

    [MaxLength(256)]
    public string? PingUrl { get; set; }
}