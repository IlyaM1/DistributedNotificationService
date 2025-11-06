using MassTransit;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Services;

public class NotificationPublisher(IBus bus) : INotificationPublisher
{
    public async Task PublishAsync<TNotificationMessage>(TNotificationMessage message, CancellationToken cancellationToken = default) 
        where TNotificationMessage : INotificationMessage
    {
        var sendEndpoint = await bus.GetSendEndpoint(new Uri($"queue:{message.QueueName}.send"));
        await sendEndpoint.Send(message, cancellationToken);
    }
}
