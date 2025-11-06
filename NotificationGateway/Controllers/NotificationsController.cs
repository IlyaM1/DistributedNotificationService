using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Dtos;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Controllers;

[ApiController]
[Route("api/v1/notifications")]
public class NotificationsController(INotificationHandlersFactory handlersFactory) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request, CancellationToken cancellationToken)
    {
        if (!handlersFactory.IsSupported(request.Type))
        {
            throw new InvalidOperationException($"No suuport for {request.Type} notification type yet");
        }

        var handler = handlersFactory.GetHandler(request.Type);
        await handler.SendNotification(request, cancellationToken);

        return new OkResult();
    }
}
