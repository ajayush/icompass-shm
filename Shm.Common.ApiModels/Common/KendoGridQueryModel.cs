using System.ComponentModel;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

/// <summary>
/// Query model that Kendo sends
/// </summary>
public class KendoGridQueryModel
{
    /// <summary>
    /// Number of results to fetch
    /// </summary>
    [DefaultValue(20)]
    public int Take { get; set; }

    /// <summary>
    /// Number of results to fetch in this page
    /// </summary>
    [DefaultValue(0)]
    public int Skip { get; set; }

    /// <summary>
    /// List of sort conditions to apply
    /// </summary>
    public List<KendoSortModel> Sort { get; set; } = new();
}