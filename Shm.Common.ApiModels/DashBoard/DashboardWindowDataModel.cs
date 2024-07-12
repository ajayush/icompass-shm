namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;

public class DashboardWindowDataModel
{
    public string TableName { get; set; } = default!;

    public string ColumnName { get; set; } = default!;

    public string DataName { get; set; } = default!;

    public string BridgeName { get; set; } = default!;

    public DateTime? StartDateTime => Timestamps.FirstOrDefault();

    public DateTime? EndDateTime => Timestamps.LastOrDefault();

    public long NumberOfPoints => Values.Count;

    public float Sum => Values.Sum(a => a ?? 0);

    public float? Average => NumberOfPoints == 0 ? null : Sum / NumberOfPoints;

    public float? Median => NumberOfPoints == 0 ? null : Values.OrderBy(a => a).ElementAt((int)NumberOfPoints / 2);

    public float? Mode => NumberOfPoints == 0 ? null : Values.GroupBy(a => a).OrderByDescending(a => a.Count()).First().Key;

    public float? Min => NumberOfPoints == 0 ? null : Values.Min();

    public float? Max => NumberOfPoints == 0 ? null : Values.Max();

    public float? First => NumberOfPoints == 0 ? null : Values.First();

    public float? Last => NumberOfPoints == 0 ? null : Values.Last();

    public void AddPoint(float? value, DateTime timestamp)
    {
        Clean();
        Values.AddLast(value);
        Timestamps.AddLast(timestamp);
    }

    public void Clean()
    {
        if (NumberOfPoints >= MaxNumberOfPoints)
        {
            Values.RemoveFirst();
            Timestamps.RemoveFirst();
        }

        while (MinDateTime.HasValue && Timestamps.Any() && Timestamps.First() < MinDateTime)
        {
            Values.RemoveFirst();
            Timestamps.RemoveFirst();
        }
    }

    public LinkedList<float?> Values { get; } = new();

    public LinkedList<DateTime> Timestamps { get; } = new();

    public long MaxNumberOfPoints { get; set; }

    public TimeSpan? MaxInterval { get; set; }

    public DateTime? MinDateTime => MaxInterval.HasValue && EndDateTime.HasValue ? EndDateTime.Value.Subtract(MaxInterval.Value) : null;
}