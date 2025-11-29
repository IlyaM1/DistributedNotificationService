namespace NotificationGateway.Dtos.Sms;

public record SmsNotification(
    Guid Id,
    string PhoneNumber,
    string Message,
    DateTime CreatedAt
);