namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;

public class BatchSingleColumnDataListModel
{
    public string TableName { get; set; } = default!;
    
    public string ColumnName { get; set; } = default!;

    public string DataName { get; set; } = default!;

    public List<float?> Values { get; set; } = new();
}