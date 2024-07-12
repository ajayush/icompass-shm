using BridgeIntelligence.CommonCore.ApiBase.Controllers;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Services;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.ApiControllers;

public class ServicesController(IIxLogger logger, IApiRequestContext apiRequestContext, ServiceService service)
    : BaseApiController(logger, apiRequestContext)
{
    [HttpGet]
    [Route("")]
    public async Task<CommonListModel<ServiceReturnModel>> GetAsync(
        CancellationToken cancellationToken, bool includeDeleted = false, bool includeInactive = false)
    {
        return await service.GetAsync(includeDeleted, includeInactive, cancellationToken)
            .ConfigureAwait(false);
    }
}