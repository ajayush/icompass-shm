namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

public class GridQueryModel<T> : KendoGridQueryModel
{

    /// <summary>
    /// Custom filters that can be applied on the item
    /// </summary>
    public T? CustomFilters { get; set; }
}