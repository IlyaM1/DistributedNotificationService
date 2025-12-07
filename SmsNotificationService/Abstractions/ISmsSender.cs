using NotificationGateway.Dtos.Sms;
using SmsNotificationService.Enums;

namespace SmsNotificationService.Abstractions;

public interface ISmsSender
{
    public Task<SendSmsStatusEnum> SendSmsAsync(SmsNotification sms);
}
