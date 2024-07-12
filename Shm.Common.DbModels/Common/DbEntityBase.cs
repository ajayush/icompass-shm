using System.ComponentModel;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

public abstract class DbEntityBase
{
    public string? ItemCreatedById { get; set; }

    public string? ItemLastModifiedById { get; set; }

    /// <summary>
    /// Date/time when the item was created
    /// </summary>
    public DateTimeOffset DateCreated { get; set; }

    /// <summary>
    /// Date/time when the item was last modified
    /// </summary>
    public DateTimeOffset DateLastModified { get; set; }

    /// <summary>
    /// If the item is deleted or not
    /// </summary>
    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
}