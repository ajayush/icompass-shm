namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.SensorOutputSummary;

public class SensorOutputSummaryQueryReturnModel
{
    public string Id { get; set; } = default!;

    public int BridgeId { get; set; }

    public string BridgeName { get; set; } = default!;

    public int SensorOutputId { get; set; }

    public string SensorOutputName { get; set; } = default!;

    public float? Minimum { get; set; }

    public float? Maximum { get; set; }

    public float? Average { get; set; }

    public float? Median { get; set; }

    public float? StandardDev { get; set; }

    public float? FirstQuartile { get; set; }

    public float? ThirdQuartile { get; set; }

    public DateTimeOffset DateCreated { get; set; }
}