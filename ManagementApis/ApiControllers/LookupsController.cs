using BridgeIntelligence.CommonCore.ApiBase.Controllers;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Common;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.ApiControllers;

public class LookupsController(IIxLogger logger, IApiRequestContext apiRequestContext, LookupsService dataParserService)
    : BaseApiController(logger, apiRequestContext)
{
    [HttpGet("SpansOrPiers")]
    public async Task<CommonListModel<SelectListItem>> GetSpanOrPierAsync(CancellationToken cancellationToken)
    {
        return await dataParserService.GetSpanOrPierAsync(cancellationToken).ConfigureAwait(false);
    }

    [HttpGet("UsDs")]
    public async Task<CommonListModel<SelectListItem>> GetUsDsAsync(CancellationToken cancellationToken)
    {
        return await dataParserService.GetUsDsAsync(cancellationToken).ConfigureAwait(false);
    }

    [HttpGet("Bridges")]
    public async Task<CommonListModel<SelectListItem>> GetBridgesAsync(CancellationToken cancellationToken)
    {
        return await dataParserService.GetBridgesAsync(cancellationToken).ConfigureAwait(false);
    }

    [HttpGet("System")]
    public async Task<CommonListModel<SelectListItem>> GetSystemAsync(CancellationToken cancellationToken)
    {
        return await dataParserService.GetSystemAsync(cancellationToken).ConfigureAwait(false);
    }
}