using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class Daq : DbEntityBase
{
    [Key]
    public int Id { get; set; }

    public int BridgeId { get; set; }

    [ForeignKey("BridgeId")]
    public virtual Bridge Bridge { get; set; } = default!;

    [MaxLength(64)]
    public string Name { get; set; } = default!;

    [MaxLength(64)]
    public string? UpstreamDownstream { get; set; }

    [MaxLength(64)]
    public string? Model { get; set; }

    [MaxLength(64)]
    public string? SpanOrPier { get; set; }

    [MaxLength(64)]
    public string? Location { get; set; }

    [MaxLength(256)]
    public string? PingUrl { get; set; }

    public bool IsFailedState { get; set; }
    
    public bool NotifiedForFailure { get; set; }

    public DateTimeOffset? LastSuccessTime { get; set; }

    public bool IsActive { get; set; }

    public virtual List<Sensor> Sensors { get; set; } = new();

    //public List<DaqFailure> DaqFailures { get; set; } = new();

    //public int? LastDaqFailureId { get; set; }

    //[ForeignKey("LastDaqFailureId")]
    //public virtual DaqFailure? LastDaqFailure { get; set; }
}