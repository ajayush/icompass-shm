using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class TableState
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [MaxLength(100)]
    [Required]
    public string TableName { get; set; } = default!;

    public DateTime LastProcessedTime { get; set; }

    public long LastRecNum { get; set; }
}