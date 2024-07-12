using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;

public class AlertRuleReturnModel : CommonReturnModel
{
    public int Id { get; set; }

    public int SensorOutputId { get; set; }

    public string? ToEmailAddresses { get; set; }

    public string RuleName { get; set; } = default!;

    public string? Description { get; set; }

    public string? ExpressionString { get; set; }

    public bool IsFailedState { get; set; }

    public string AlertType { get; set; } = default!;

    public bool IsActive { get; set; }
}