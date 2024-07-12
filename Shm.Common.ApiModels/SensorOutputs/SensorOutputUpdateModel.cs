using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;

public class SensorOutputUpdateModel
{
    [MaxLength(64)]
    public string? OutputType { get; set; }

    [MaxLength(64)]
    public string TableName { get; set; } = default!;

    [MaxLength(64)]
    public string ColumnName { get; set; } = default!;

    [MaxLength(64)]
    public string? SamplingFrequency { get; set; }

    [MaxLength(64)]
    public string? Unit { get; set; }

    public float? UpperBound { get; set; }

    public float? LowerBound { get; set; }
}