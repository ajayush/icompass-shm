namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;

public class BatchDataModel
{
    public List<DateTime> Timestamps { get; set; } = new();

    public List<long> RecNumbers { get; set; } = new();

    public List<BatchSingleColumnDataListModel> DataList { get; set; } = new();
}