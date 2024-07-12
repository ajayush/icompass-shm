using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Sensors;

public class SensorReturnModel : CommonReturnModel
{
    public int Id { get; set; }

    public int DaqId { get; set; }

    public string Name { get; set; } = default!;

    public string? AlternateName { get; set; }

    public string? Abbreviation { get; set; }

    public string? System { get; set; }

    public string? Model { get; set; }

    public string? SpanOrPier { get; set; }

    public string? UpstreamDownstream { get; set; }

    public string? Member { get; set; }

    public string? Position { get; set; }

    public bool IsActive { get; set; }
}