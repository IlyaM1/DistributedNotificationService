using MassTransit;
using NotificationGateway.Dtos.Sms;

namespace SmsNotificationService.Services;

public class SmsNotificationConsumer(ILogger<SmsNotificationConsumer> logger) : IConsumer<SmsNotification>
{
    public async Task Consume(ConsumeContext<SmsNotification> context)
    {
        logger.LogInformation("Получено сообщение: {Message}", context.Message.Message);
        await Task.CompletedTask;
    }
}
