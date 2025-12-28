using MassTransit;
using NotificationGateway.Dtos;
using NotificationGateway.Handlers;

namespace NotificationGateway.Consumers;

public class EmailResultConsumer(EmailNotificationHandler emailNotificationHandler, ILogger<EmailResultConsumer> logger) : IConsumer<BrokerResponseDto>
{
    public async Task Consume(ConsumeContext<BrokerResponseDto> context)
    {
        var brokerResponse = context.Message;
        logger.LogInformation("Got message from notifications.email.result for notification with id {Id} at {Timestamp}", brokerResponse.Id, brokerResponse.Timestamp);
        await emailNotificationHandler.ConsumeResult(brokerResponse, default);
    }
}