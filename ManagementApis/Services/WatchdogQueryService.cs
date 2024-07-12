using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.Services;

public class WatchdogQueryService(IWatchdogQueryManager watchdogQueryManager)
{
    public Task<CommonListModel<DaqGridReturnModel>> QueryAsync(WatchdogDaqQueryModel model, CancellationToken cancellationToken)
    {
        return watchdogQueryManager.QueryAsync(model, cancellationToken);
    }

    public Task<CommonListModel<SensorGridReturnModel>> QueryAsync(WatchdogSensorQueryModel model, CancellationToken cancellationToken)
    {
        return watchdogQueryManager.QueryAsync(model, cancellationToken);
    }

    public Task<CommonListModel<AlertGridReturnModel>> QueryAsync(WatchdogAlertQueryModel model, CancellationToken cancellationToken)
    {
        return watchdogQueryManager.QueryAsync(model, cancellationToken);
    }
}