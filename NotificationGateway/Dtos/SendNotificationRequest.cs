using NotificationGateway.Enums;

namespace NotificationGateway.Dtos;

public record SendNotificationRequest(
    NotificationTypeEnum Type  
);
