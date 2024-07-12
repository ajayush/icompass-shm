using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class AlertRule : DbEntityBase
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Sensor output for which this alert rule is associated with
    /// </summary>
    [ForeignKey("SensorOutputId")]
    public virtual SensorOutput SensorOutput { get; set; } = default!;

    /// <summary>
    /// Sensor output id
    /// </summary>
    public int SensorOutputId { get; set; }

    /// <summary>
    /// Email addresses to which emails will be sent upon failures
    /// </summary>
    public string? ToEmailAddresses { get; set; }

    /// <summary>
    /// Rule name
    /// </summary>
    [MaxLength(128)]
    public string RuleName { get; set; } = default!;

    /// <summary>
    /// Rule Description
    /// </summary>
    [MaxLength(4096)]
    public string? Description { get; set; }

    /// <summary>
    /// Expression string to evaluate
    /// </summary>
    public string? ExpressionString { get; set; }

    /// <summary>
    /// Is alert rule in failed state
    /// </summary>
    public bool IsFailedState { get; set; }

    /// <summary>
    /// Is alert rule active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Alert type
    /// </summary>
    public string AlertType { get; set; } = default!;

    public List<AlertRuleFailure> AlertRuleFailures { get; set; } = new();

    public int? LastAlertRuleFailureId { get; set; }

    [ForeignKey("LastAlertRuleFailureId")]
    public virtual AlertRuleFailure? LastAlertRuleFailure { get; set; }
}