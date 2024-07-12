using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class Sensor : DbEntityBase
{
    [Key]
    public int Id { get; set; }

    public int DaqId { get; set; }

    [ForeignKey("DaqId")]
    public virtual Daq Daq { get; set; } = default!;

    [MaxLength(64)]
    public string Name { get; set; } = default!;

    [MaxLength(256)]
    public string? AlternateName { get; set; }

    [MaxLength(32)]
    public string? Abbreviation { get; set; }

    [MaxLength(64)]
    public string? System { get; set; }

    [MaxLength(64)]
    public string? Model { get; set; }

    [MaxLength(64)]
    public string? SpanOrPier { get; set; }

    [MaxLength(64)]
    public string? UpstreamDownstream { get; set; }

    [MaxLength(64)]
    public string? Member { get; set; }

    [MaxLength(64)]
    public string? Position { get; set; }

    public bool IsActive { get; set; }

    public List<SensorOutput> SensorOutputs { get; set; } = new();

    //public List<SensorFailure> SensorFailures { get; set; } = new();

    //public int? LastSensorFailureId { get; set; }

    //[ForeignKey("LastSensorFailureId")]
    //public virtual SensorFailure? LastSensorFailure { get; set; }
}