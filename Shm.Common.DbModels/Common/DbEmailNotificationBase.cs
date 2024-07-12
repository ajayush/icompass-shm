using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

public class DbEmailNotificationBase
{
    /// <summary>
    /// Date/time when the item was created
    /// </summary>
    public DateTimeOffset DateCreated { get; set; }

    public string Body { get; set; } = default!;

    [MaxLength(1024)]
    public string Subject { get; set; } = default!;

    [MaxLength(1024)]
    public string To { get; set; } = default!;

    [MaxLength(64)]
    public string DeliveryStatus { get; set; } = default!;
}