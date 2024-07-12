using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class DaqFailure
{
    [Key]
    public int Id { get; set; }

    public int DaqId { get; set; }

    [ForeignKey("DaqId")]
    public virtual Daq Daq { get; set; } = default!;

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset? EndTime { get; set; }

    public bool IsNotificationSentForFailure { get; set; }

    public bool IsNotificationSentForResolved { get; set; }

    /// <summary>
    /// Email addresses to which emails will be sent upon failures
    /// </summary>
    public string? ToEmailAddresses { get; set; }
}