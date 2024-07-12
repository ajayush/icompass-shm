namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

public abstract class CommonReturnModel
{
    /// <summary>
    /// User id of the user who created the item
    /// </summary>
    public string? ItemCreatedById { get; set; }

    /// <summary>
    /// User id of user who modified the item last
    /// </summary>
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
    public bool IsDeleted { get; set; }
}