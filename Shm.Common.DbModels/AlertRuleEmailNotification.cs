using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class AlertRuleEmailNotification : DbEmailNotificationBase
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Alert rule id for which notification was sent
    /// </summary>
    public int AlertRuleId { get; set; }

    /// <summary>
    /// Alert rule for which notification was sent
    /// </summary>
    [ForeignKey("AlertRuleId")]
    public virtual AlertRule AlertRule { get; set; } = default!;
}