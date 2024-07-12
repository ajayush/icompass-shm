using BridgeIntelligence.CommonCore.ApiBase.Controllers;
using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.DashBoard;
using BridgeIntelligence.iCompass.Shm.Management.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIntelligence.iCompass.Shm.Management.Api.ApiControllers;

public class DashboardController(IIxLogger logger, IApiRequestContext apiRequestContext, IDashboardService service)
    : BaseApiController(logger, apiRequestContext)
{
    [HttpGet]
    [Route("BBB")]
    public BurlingtonDashboardModel GetAsync()
    {
        return service.GetForBurlington();
    }
    [HttpGet]
    [Route("TPB")]
    public TaconyDashboardModel GetTaconyData()
    {
        return service.GetTaconyData();
    }
    [HttpGet]
    [Route("RDB")]
    public RiversideDashboardModel GetRiversideData()
    {
        return service.GetRiversideData();
    }
}