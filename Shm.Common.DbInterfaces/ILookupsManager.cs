using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface ILookupsManager
{
    Task<CommonListModel<SelectListItem>> GetSpanOrPierAsync(CancellationToken cancellationToken);

    Task<CommonListModel<SelectListItem>> GetUsDsAsync(CancellationToken cancellationToken);
    
    Task<CommonListModel<SelectListItem>> GetSystemAsync(CancellationToken cancellationToken);
}