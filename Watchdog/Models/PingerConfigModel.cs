namespace BridgeIntelligence.iCompass.Shm.Watchdog.Api.Models;

public class PingerConfigModel
{
    public bool EnabledForDaqs { get; set; }

    public bool EnabledForServices { get; set; }

    public int IntervalInSeconds { get; set; }
}