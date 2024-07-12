namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputSummary;

public class SensorOutputSummaryReturnModel
{
    public string Id { get; set; } = default!;

    public int BridgeId { get; set; }

    public int SensorOutputId { get; set; }

    public float? Minimum { get; set; }

    public float? Maximum { get; set; }

    public float? Average { get; set; }

    public float? Median { get; set; }

    public float? StandardDev { get; set; }

    public float? FirstQuartile { get; set; }

    public float? ThirdQuartile { get; set; }

    public DateTimeOffset DateCreated { get; set; }
}