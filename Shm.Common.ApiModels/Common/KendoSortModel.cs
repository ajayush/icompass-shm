namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

/// <summary>
/// Sort model for Kendo
/// </summary>
public class KendoSortModel
{
    /// <summary>
    /// Search string
    /// </summary>
    public KendoSortDirection Dir { get; set; }

    /// <summary>
    /// Field to search on
    /// </summary>
    public string Field { get; set; } = default!;
}