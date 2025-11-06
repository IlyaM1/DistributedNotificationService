using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Dtos;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Controllers;

[ApiController]
[Route("api/v1/notifications")]
public class NotificationsController(INotificationPublisher publisher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationMessage(
            Type: request.Type,
            Recipient: request.Recipient,
            Message: request.Message,
            request.Metadata,
            DateTime.UtcNow
        );

        await publisher.PublishAsync(notificationMessage, cancellationToken);
        return new OkResult();
    }
}
