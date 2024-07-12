namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputSummary;

public class SensorOutputSummaryQueryModel
{
    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }

    public int? SensorOutputId { get; set; }

    public int? BridgeId { get; set; }
}