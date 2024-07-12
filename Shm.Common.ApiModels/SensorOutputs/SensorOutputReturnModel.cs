using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputs;

public class SensorOutputReturnModel : CommonReturnModel
{
    public int Id { get; set; }

    public int SensorId { get; set; }

    public string? OutputType { get; set; }
    
    public string TableName { get; set; } = default!;

    public string ColumnName { get; set; } = default!;

    public string? Unit { get; set; }
    
    public string? SamplingFrequency { get; set; }

    public float? UpperBound { get; set; }

    public float? LowerBound { get; set; }

    public bool IsActive { get; set; }

    public bool IsFailedState { get; set; }

    public DateTimeOffset? LastSuccessTime { get; set; }
}