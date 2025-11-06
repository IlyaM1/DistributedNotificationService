using NotificationGateway.Dtos;

namespace NotificationGateway.Interfaces;

public interface INotificationPublisher
{
    public Task PublishAsync(NotificationMessage message, CancellationToken cancellationToken = default);
}
