using BridgeIntelligence.CommonCore.ApiBase.Controllers;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.ApiControllers;

public class WatchdogQueryController(IIxLogger logger, IApiRequestContext apiRequestContext, WatchdogQueryService service)
    : BaseApiController(logger, apiRequestContext)
{
    [HttpPost("Daqs")]
    public async Task<CommonListModel<DaqGridReturnModel>> QueryAsync(WatchdogDaqQueryModel model,
        CancellationToken cancellationToken)
    {
        return await service.QueryAsync(model, cancellationToken).ConfigureAwait(false);
    }

    [HttpPost("Alerts")]
    public async Task<CommonListModel<AlertGridReturnModel>> QueryAsync(WatchdogAlertQueryModel model,
        CancellationToken cancellationToken)
    {
        return await service.QueryAsync(model, cancellationToken).ConfigureAwait(false);

    }

    [HttpPost("Sensors")]
    public async Task<CommonListModel<SensorGridReturnModel>> QueryAsync(WatchdogSensorQueryModel model,
        CancellationToken cancellationToken)
    {
        return await service.QueryAsync(model, cancellationToken).ConfigureAwait(false);
    }
}