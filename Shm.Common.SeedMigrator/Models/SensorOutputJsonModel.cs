namespace BridgeIntelligence.iCompass.Shm.Common.SeedMigrator.Models;

internal class SensorOutputJsonModel
{
    public string SensorName { get; set; } = default!;

    public string OutputType { get; set; } = default!;

    public string TableName { get; set; } = default!;

    public string ColumnName { get; set; } = default!;

    public string SamplingFrequency { get; set; } = default!;

    public string Unit { get; set; } = default!;

    public float? UpperBound { get; set; } = default!;

    public float? LowerBound { get; set; } = default!;
}