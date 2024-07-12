namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Health;

public class ServiceHealthStatusApiModel
{
    public string ServiceName { get; set; } = default!;

    public bool IsHealthy { get; set; }
}