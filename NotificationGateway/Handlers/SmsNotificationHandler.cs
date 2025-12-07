using NotificationGateway.Database.Models;
using NotificationGateway.Dtos;
using NotificationGateway.Dtos.Sms;
using NotificationGateway.Enums;
using NotificationGateway.Helpers;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Handlers;

public class SmsNotificationHandler(INotificationPublisher publisher, INotificationRepository notificationRepository) : INotificationHandler
{
    public NotificationTypeEnum Type => NotificationTypeEnum.Sms;

    public async Task SendNotification(NotificationModel notification, CancellationToken cancellationToken = default)
    {
        var smsMessage = new SmsNotificationMessage(
            Id: notification.Id,
            PhoneNumber: notification.Recipient,
            Message: notification.Message,
            CreatedAt: DateTime.UtcNow
        );

        await publisher.PublishAsync(smsMessage, new Uri($"queue:notifications.sms.send"), cancellationToken);
    }

    public async Task ConsumeResult(BrokerResponseDto responseDto, CancellationToken cancellationToken = default)
    {
        var status = EnumHelper.ParseFromString<StatusEnum>(responseDto.Status);
        await notificationRepository.UpdateStatusAsync(responseDto.Id, status);

        // TODO: add retry policy
    }
}
