using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class Service
{
    [Key]
    public int Id { get; set; }

    public string ServiceName { get; set; } = default!;

    public bool IsFailedState { get; set; }

    public DateTimeOffset? LastSuccessTime { get; set; }

    public bool IsWorker { get; set; }

    public string? PingUrl { get; set; }

    public bool IsActive { get; set; }

    public bool NotifiedForFailure { get; set; }

    public bool IsDeleted { get; set; }
}