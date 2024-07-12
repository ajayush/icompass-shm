using BridgeIntelligence.CommonCore.EmailSender;

namespace BridgeIntelligence.iCompass.Shm.AlertRuleEngine.Api.Config;

public class AlertRuleWorkerConfig
{
    public int CycleIntervalInSeconds { get; set; } = 300;

    public int MaxFailuresInCycle { get; set; } = 100;

    public EmailDetail FromEmail { get; set; } = new EmailDetail
    {
        Email = "alerts@icompass.website",
        Name = "iCompass Alerts"
    };
}