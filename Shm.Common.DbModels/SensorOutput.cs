using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class SensorOutput : DbEntityBase
{
    [Key]
    public int Id { get; set; }

    public int SensorId { get; set; }

    [ForeignKey("SensorId")]
    public virtual Sensor Sensor { get; set; } = default!;

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

    public bool IsFailedState { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset? LastSuccessTime { get; set; }

    public List<AlertRule> AlertRules { get; set; } = new();
}