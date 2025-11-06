using NotificationGateway.Enums;

namespace NotificationGateway.Dtos;

public record NotificationMessage(
    NotificationTypeEnum Type,
    string Recipient,
    string Message,
    List<string>? Metadata,
    DateTime CreatedAt
);