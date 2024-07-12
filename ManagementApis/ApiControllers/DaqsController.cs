using BridgeIntelligence.CommonCore.ApiBase.Controllers;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Daqs;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.ApiControllers;

public class DaqsController(IIxLogger logger, IApiRequestContext apiRequestContext,
        DaqService service)
    : BaseApiController(logger, apiRequestContext)
{
    [HttpPost]
    [Route("")]
    public async Task<DaqReturnModel> CreateAsync(DaqCreateModel model,
        CancellationToken cancellationToken)
    {
        return await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
    }

    [HttpGet]
    [Route("")]
    public async Task<CommonListModel<DaqReturnModel>> GetAsync(
        CancellationToken cancellationToken, bool includeDeleted = false, bool includeInactive = false)
    {
        return await service.GetAsync(includeDeleted, includeInactive, cancellationToken)
            .ConfigureAwait(false);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<DaqReturnModel> UpdateAsync(int id, DaqUpdateModel model,
        CancellationToken cancellationToken)
    {
        return await service.UpdateAsync(id, model, cancellationToken).ConfigureAwait(false);
    }

    [HttpPut]
    [Route("{id}/IsActive")]
    public async Task<bool> ChangeActiveStatusAsync(int id, bool isActive,
        CancellationToken cancellationToken)
    {
        return await service.ChangeActiveStatusAsync(id, isActive, cancellationToken).ConfigureAwait(false);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
    }
}