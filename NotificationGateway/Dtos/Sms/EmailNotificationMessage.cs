using NotificationGateway.Interfaces;

namespace NotificationGateway.Dtos.Sms;

public record EmailNotificationMessage(
    Guid Id,
    string Email,
    string Theme,
    string Message,
    Dictionary<string, string> Attachments,
    DateTime CreatedAt
) : INotificationMessage;