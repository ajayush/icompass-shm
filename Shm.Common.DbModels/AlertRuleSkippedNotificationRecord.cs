using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class AlertRuleSkippedNotificationRecord
{
    [Key]
    public int Id { get; set; }

    public DateTimeOffset DateCreated { get; set; }

    public int AlertRuleId { get; set; }

    public int EmailNotificationId { get; set; }
}