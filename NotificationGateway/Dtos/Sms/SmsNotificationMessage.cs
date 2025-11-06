using NotificationGateway.Interfaces;

namespace NotificationGateway.Dtos.Sms;

public record SmsNotificationMessage(
    Guid Id,
    string PhoneNumber,
    string Message,
    DateTime CreatedAt
) : INotificationMessage
{
    public string QueueName => "notifications.sms";
}
