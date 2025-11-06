namespace NotificationGateway.Interfaces;

public interface INotificationPublisher
{
    public Task PublishAsync<TNotificationMessage>(TNotificationMessage message, CancellationToken cancellationToken = default)
        where TNotificationMessage : INotificationMessage;
}
