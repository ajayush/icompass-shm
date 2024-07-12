using BridgeIntelligence.CommonCore.ApiBase.Controllers;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Alerts;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.ApiControllers;

public class AlertRulesController(IIxLogger logger, IApiRequestContext apiRequestContext, AlertRuleService service)
    : BaseApiController(logger, apiRequestContext)
{
    [HttpPost]
    [Route("")]
    public async Task<AlertRuleReturnModel> CreateAsync(AlertRuleCreateModel model,
        CancellationToken cancellationToken)
    {
        return await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
    }

    [HttpGet]
    [Route("")]
    public async Task<CommonListModel<AlertRuleReturnModel>> GetAsync(
        CancellationToken cancellationToken, bool includeDeleted = false, bool includeInactive = false)
    {
        return await service.GetAsync(includeDeleted, includeInactive, cancellationToken)
            .ConfigureAwait(false);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<AlertRuleReturnModel> UpdateAsync(int id, AlertRuleUpdateModel model,
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