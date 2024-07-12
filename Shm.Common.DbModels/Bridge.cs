using System.ComponentModel.DataAnnotations;
using BridgeIntelligence.iCompass.Shm.Common.DbModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.DbModels;

public class Bridge : DbEntityBase
{
    [Key]
    public int Id { get; set; }

    [MaxLength(10)]
    public string BridgeAbbreviation { get; set; } = default!;

    [MaxLength(64)]
    public string BridgeName { get; set; } = default!;
}