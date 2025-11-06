using NotificationGateway.Enums;

namespace NotificationGateway.Dtos;

public record SendNotificationRequest(
    NotificationTypeEnum Type,
    string Recipient,
    string Message,
    Dictionary<string, string>? Metadata
);
