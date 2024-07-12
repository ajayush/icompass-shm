using BridgeIntelligence.CommonCore.ApiBase.Controllers;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Bridges;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.ApiControllers;

public class BridgesController(IIxLogger logger, IApiRequestContext apiRequestContext, BridgeService service)
    : BaseApiController(logger, apiRequestContext)
{
    [HttpPost]
    [Route("")]
    public async Task<BridgeReturnModel> CreateAsync(BridgeCreateModel model,
        CancellationToken cancellationToken)
    {
        return await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
    }

    [HttpGet]
    [Route("")]
    public async Task<CommonListModel<BridgeReturnModel>> GetAsync(
        CancellationToken cancellationToken, bool includeDeleted = false)
    {
        return await service.GetAsync(includeDeleted, cancellationToken)
            .ConfigureAwait(false);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
    }
}