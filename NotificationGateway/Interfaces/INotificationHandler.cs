using NotificationGateway.Database.Models;
using NotificationGateway.Dtos;
using NotificationGateway.Enums;

namespace NotificationGateway.Interfaces;

public interface INotificationHandler
{
    public NotificationTypeEnum Type { get; }

    public Task SendNotification(NotificationModel notification, CancellationToken cancellationToken = default);
    public Task ConsumeResult(BrokerResponseDto responseDto, CancellationToken cancellationToken = default);
}
