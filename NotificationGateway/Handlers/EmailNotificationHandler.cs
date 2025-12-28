using NotificationGateway.Database.Models;
using NotificationGateway.Dtos;
using NotificationGateway.Dtos.Sms;
using NotificationGateway.Enums;
using NotificationGateway.Helpers;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Handlers;

public class EmailNotificationHandler(INotificationPublisher publisher, INotificationRepository notificationRepository, IConfiguration configuration) : INotificationHandler
{
    public NotificationTypeEnum Type => NotificationTypeEnum.Email;

    public async Task SendNotification(NotificationModel notification, CancellationToken cancellationToken = default)
    {
        var subject = notification.Metadata?.TryGetValue("Theme", out var subj) == true ? subj : "Without theme";
        // TODO: add work with attachments

        var emailMessage = new EmailNotificationMessage(
            Id: notification.Id,
            Email: notification.Recipient,
            Theme: subject,
            Message: notification.Message,
            Attachments: [],
            CreatedAt: DateTime.UtcNow
        );

        await publisher.PublishAsync(emailMessage, new Uri("queue:notifications.email.send"), cancellationToken);
    }

    public async Task ConsumeResult(BrokerResponseDto responseDto, CancellationToken cancellationToken = default)
    {
        var status = EnumHelper.ParseFromString<StatusEnum>(responseDto.Status);
        await notificationRepository.UpdateStatusAsync(responseDto.Id, status);

        if (status is StatusEnum.Failed)
        {
            var retriesAmount = await notificationRepository.GetAmountOfRetries(responseDto.Id);
            if (retriesAmount >= int.Parse(configuration["AmountOfRetries"]!))
            {
                await notificationRepository.UpdateStatusAsync(responseDto.Id, StatusEnum.PermanentlyFailed);
            }
            else
            {
                await notificationRepository.UpdateStatusAsync(responseDto.Id, StatusEnum.Retrying);
                await notificationRepository.IncrementAmountOfRetries(responseDto.Id);
                var notificationToResend = await notificationRepository.GetAsync(responseDto.Id);
                await SendNotification(notificationToResend, cancellationToken);
            }
        }
    }
}