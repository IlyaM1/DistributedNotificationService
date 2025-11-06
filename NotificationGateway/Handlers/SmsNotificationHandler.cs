using NotificationGateway.Dtos;
using NotificationGateway.Dtos.Sms;
using NotificationGateway.Enums;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Handlers;

public class SmsNotificationHandler(INotificationPublisher publisher) : INotificationHandler
{
    public NotificationTypeEnum Type => NotificationTypeEnum.Sms;

    public async Task SendNotification(SendNotificationRequest request, CancellationToken cancellationToken = default)
    {
        var smsMessage = new SmsNotificationMessage(
            Id: Guid.NewGuid(),
            PhoneNumber: request.Recipient,
            Message: request.Message,
            CreatedAt: DateTime.UtcNow
        );

        await publisher.PublishAsync(smsMessage, cancellationToken);
    }
}
