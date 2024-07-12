using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;

public class AlertRuleCreateModel
{
    public int SensorOutputId { get; set; }

    public string? ToEmailAddresses { get; set; }

    [Required]
    public string RuleName { get; set; } = default!;

    public string? Description { get; set; }

    public string? ExpressionString { get; set; }

    public AlertRuleTypes AlertType { get; set; }
}