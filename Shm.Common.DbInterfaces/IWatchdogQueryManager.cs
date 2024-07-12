using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;

namespace BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

public interface IWatchdogQueryManager
{
    Task<CommonListModel<DaqGridReturnModel>> QueryAsync(WatchdogDaqQueryModel model,
        CancellationToken cancellationToken);

    Task<CommonListModel<SensorGridReturnModel>> QueryAsync(WatchdogSensorQueryModel model,
        CancellationToken cancellationToken);

    Task<CommonListModel<AlertGridReturnModel>> QueryAsync(WatchdogAlertQueryModel model,
        CancellationToken cancellationToken);
}