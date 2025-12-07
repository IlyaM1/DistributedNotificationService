using NotificationGateway.Dtos.Sms;
using SmsNotificationService.Abstractions;
using SmsNotificationService.Enums;
using System.Security.Cryptography;

namespace SmsNotificationService.Services;

public class MockSmsSender(ILogger<MockSmsSender> logger) : ISmsSender
{
    public async Task<SendSmsStatusEnum> SendSmsAsync(SmsNotification sms)
    {
        // emulation of delay
        await Task.Delay(5000);

        // emulation of error
        var randomNumber = RandomNumberGenerator.GetInt32(100);
        if (randomNumber > 70)
        {
            logger.LogError("Happened error while trying to send sms for {PhoneNumber} with id {Id} including text: {Message}", sms.PhoneNumber, sms.Id, sms.Message);
            return SendSmsStatusEnum.Fail;
        }

        logger.LogInformation("Successfully sent sms for {PhoneNumber} with id {Id} including text: {Message}", sms.PhoneNumber, sms.Id, sms.Message);
        return SendSmsStatusEnum.Success;
    }
}
