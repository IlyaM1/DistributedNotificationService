using Microsoft.AspNetCore.Mvc;

namespace NotificationGateway.Controllers;

[ApiController]
[Route("api/v1/healthcheck")]
public class HealthcheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return new OkResult();
    }
}
