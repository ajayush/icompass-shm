using System.ComponentModel.DataAnnotations;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Bridges;

public class BridgeCreateModel
{
    [MaxLength(10)]
    public string BridgeAbbreviation { get; set; } = default!;

    [MaxLength(64)]
    public string BridgeName { get; set; } = default!;
}