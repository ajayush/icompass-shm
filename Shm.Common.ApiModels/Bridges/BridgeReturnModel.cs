using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.ApiModels.Bridges;

public class BridgeReturnModel : CommonReturnModel
{
    public int Id { get; set; }

    public string BridgeAbbreviation { get; set; } = default!;

    public string BridgeName { get; set; } = default!;
}