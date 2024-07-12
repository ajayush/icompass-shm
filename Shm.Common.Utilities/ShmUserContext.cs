using BridgeIntelligence.CommonCore.Models;
using Microsoft.AspNetCore.Http;

namespace BridgeIntelligence.iCompass.Shm.Common.Utilities;

public class ShmUserContext : UserContextModel
{
    public ShmUserContext(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("BI-UserId") == true &&
            httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("BI-UserName") == true)
        {
            UserId = httpContextAccessor.HttpContext.Request.Headers["BI-UserId"];
            Username = httpContextAccessor.HttpContext.Request.Headers["BI-UserName"];
        }
    }
}