namespace NotificationGateway.Interfaces;

public interface INotificationPublisher
{
    public Task PublishAsync<TNotificationMessage>(
        TNotificationMessage message,
        Uri sendEndpointUri,
        CancellationToken cancellationToken = default
    ) where TNotificationMessage : INotificationMessage;
}
