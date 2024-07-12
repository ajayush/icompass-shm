using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class SensorFailure
{
    [Key]
    public int Id { get; set; }

    public int SensorId { get; set; }

    [ForeignKey("SensorId")]
    public virtual Sensor Sensor { get; set; } = default!;

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset? EndTime { get; set; }

    public bool IsNotificationSentForFailure { get; set; }

    public bool IsNotificationSentForResolved { get; set; }

    /// <summary>
    /// Email addresses to which emails will be sent upon failures
    /// </summary>
    public string? ToEmailAddresses { get; set; }
}