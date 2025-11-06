using MassTransit;
using NotificationGateway.Dtos;
using NotificationGateway.Enums;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Services;

public class NotificationPublisher(IBus bus) : INotificationPublisher
{
    public async Task PublishAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        // TODO: change on atribute style
        var queueName = message.Type switch
        {
            NotificationTypeEnum.Email => "notifications.email",
            NotificationTypeEnum.Sms => "notifications.sms",
            _ => "notifications.unknown"
        };

        var sendEndpoint = await bus.GetSendEndpoint(new Uri($"queue:{queueName}"));
        await sendEndpoint.Send(message, cancellationToken);
    }
}
