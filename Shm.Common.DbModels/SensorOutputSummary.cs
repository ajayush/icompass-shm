using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class SensorOutputSummary
{
    [Key]
    public string Id { get; set; } = default!;

    public int BridgeId { get; set; }

    [ForeignKey("BridgeId")]
    public virtual Bridge Bridge { get; set; } = default!;

    public int SensorOutputId { get; set; }

    [ForeignKey("SensorOutputId")]
    public virtual SensorOutput SensorOutput { get; set; } = default!;

    public float? Minimum { get; set; }

    public float? Maximum { get; set; }

    public float? Average { get; set; }

    public float? Median { get; set; }

    public float? StandardDev { get; set; }

    public float? FirstQuartile { get; set; }

    public float? ThirdQuartile { get; set; }

    public DateTimeOffset DateCreated { get; set; }
}