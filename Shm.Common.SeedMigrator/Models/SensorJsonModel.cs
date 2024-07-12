namespace BridgeIntelligence.iCompass.Shm.Common.SeedMigrator.Models;

internal class SensorJsonModel
{
    public string BridgeName { get; set; } = default!;

    public string DaqName { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Abbreviation { get; set; } = default!;

    public string AlternateName { get; set; } = default!;

    public string System { get; set; } = default!;

    public string Model { get; set; } = default!;

    public string SpanOrPier { get; set; } = default!;

    public string UpstreamDownstream { get; set; } = default!;

    public string Member { get; set; } = default!;

    public string Position { get; set; } = default!;
}