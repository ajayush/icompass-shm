namespace BridgeIntelligence.iCompass.Shm.Common.SeedMigrator.Models;

internal class SeedJsonModel
{
    public List<BridgeJsonModel> Bridges { get; set; } = default!;

    public List<DaqJsonModel> Daqs { get; set; } = default!;

    public List<SensorJsonModel> Sensors { get; set; } = default!;

    public List<SensorOutputJsonModel> SensorOutputs { get; set; } = default!;
}