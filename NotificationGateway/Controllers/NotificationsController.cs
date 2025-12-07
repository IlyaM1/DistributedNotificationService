using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Database.Models;
using NotificationGateway.Dtos;
using NotificationGateway.Enums;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Controllers;

[ApiController]
[Route("api/v1/notifications")]
public class NotificationsController(
    INotificationHandlersFactory handlersFactory,
    INotificationRepository notificationRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request, CancellationToken cancellationToken)
    {
        var notification = new NotificationModel()
        {
            Id = Guid.NewGuid(),
            Status = StatusEnum.Created,
            Type = request.Type,
            Message = request.Message,
            Recipient = request.Recipient,
            Metadata = request.Metadata,
            CreatedAt = DateTime.UtcNow,
        };
        await notificationRepository.CreateAsync(notification);

        if (!handlersFactory.IsSupported(request.Type))
        {
            throw new InvalidOperationException($"No support for {request.Type} notification type yet");
        }

        var handler = handlersFactory.GetHandler(request.Type);
        await handler.SendNotification(notification, cancellationToken);

        await notificationRepository.UpdateStatusAsync(notification.Id, StatusEnum.Queued);

        return new OkObjectResult(notification.Id);
    }
}
