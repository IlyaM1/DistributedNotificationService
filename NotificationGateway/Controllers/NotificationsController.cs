using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Dtos;

namespace NotificationGateway.Controllers;

[ApiController]
[Route("api/v1/notifications")]
public class NotificationsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotification(SendNotificationRequest request)
    {
        await Task.CompletedTask;
        return new OkResult();
    } 
}
