using SmsNotificationService.Enums;

namespace SmsNotificationService.Dtos;

public record BrokerResponseDto(
    Guid Id,
    string Status,
    string? ErrorMessage,
    DateTime Timestamp,
    Dictionary<string, string>? Metadata
);
