using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Sensors;

public class SensorUpdateModel
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;

    [MaxLength(256)]
    public string? AlternateName { get; set; }

    [MaxLength(32)]
    public string? Abbreviation { get; set; }

    [MaxLength(64)]
    public string? System { get; set; }

    [MaxLength(64)]
    public string? Model { get; set; }

    [MaxLength(64)]
    public string? SpanOrPier { get; set; }

    [MaxLength(64)]
    public string? UpstreamDownstream { get; set; }

    [MaxLength(64)]
    public string? Member { get; set; }

    [MaxLength(64)]
    public string? Position { get; set; }
}