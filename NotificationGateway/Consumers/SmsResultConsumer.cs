using MassTransit;
using NotificationGateway.Dtos;
using NotificationGateway.Handlers;

namespace NotificationGateway.Consumers;

public class SmsResultConsumer(SmsNotificationHandler smsNotificationHandler, ILogger<SmsResultConsumer> logger) : IConsumer<BrokerResponseDto>
{
    public async Task Consume(ConsumeContext<BrokerResponseDto> context)
    {
        var brokerResponse = context.Message;
        logger.LogInformation("Got message from notification.sms.result for notification with id {Id} at {Timestamp}", brokerResponse.Id, brokerResponse.Timestamp);
        await smsNotificationHandler.ConsumeResult(brokerResponse, default);
    }
}
