namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;

public class TableStateReturnModel
{
    public string TableName { get; set; } = default!;

    public DateTime LastProcessedTime { get; set; }

    public long LastRecNum { get; set; }
}