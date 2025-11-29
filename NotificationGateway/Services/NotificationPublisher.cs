using MassTransit;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Services;

public class NotificationPublisher(IBus bus) : INotificationPublisher
{
    public async Task PublishAsync<TNotificationMessage>(TNotificationMessage message, Uri sendEndpointUri, CancellationToken cancellationToken = default) 
        where TNotificationMessage : INotificationMessage
    {
        var sendEndpoint = await bus.GetSendEndpoint(sendEndpointUri);
        await sendEndpoint.Send(message, cancellationToken);
    }
}
