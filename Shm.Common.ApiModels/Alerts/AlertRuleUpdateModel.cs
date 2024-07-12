namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;

public class AlertRuleUpdateModel
{
    public string? ToEmailAddresses { get; set; }

    public string RuleName { get; set; } = default!;

    public string? Description { get; set; }
}