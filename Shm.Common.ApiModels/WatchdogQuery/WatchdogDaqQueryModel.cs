using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Health;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;

public class WatchdogDaqQueryModel
{
    public int? BridgeId { get; set; }

    public HealthStatus? Status { get; set; }

    public string? System { get; set; }

    public string? UpstreamDownstream { get; set; }
}