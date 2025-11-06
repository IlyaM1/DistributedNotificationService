using NotificationGateway.Dtos;
using NotificationGateway.Enums;

namespace NotificationGateway.Interfaces;

public interface INotificationHandler
{
    public NotificationTypeEnum Type { get; }

    public Task SendNotification(SendNotificationRequest request, CancellationToken cancellationToken = default);
}
