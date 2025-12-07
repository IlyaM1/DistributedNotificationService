using MassTransit;
using NotificationGateway.Dtos.Sms;
using SmsNotificationService.Abstractions;
using SmsNotificationService.Dtos;
using SmsNotificationService.Enums;

namespace SmsNotificationService.Services;

public class SmsNotificationConsumer(ISmsSender smsSender, ILogger<SmsNotificationConsumer> logger) : IConsumer<SmsNotification>
{
    public async Task Consume(ConsumeContext<SmsNotification> context)
    {
        var sms = context.Message;
        logger.LogInformation("Got message from consumer for {PhoneNumber} with id {Id} including text: {Message}", sms.PhoneNumber, sms.Id, sms.Message);

        var processingDto = new BrokerResponseDto(
            Id: sms.Id,
            Status: StatusEnum.Processing.ToString().ToUpper(),
            ErrorMessage: null,
            Timestamp: DateTime.UtcNow,
            Metadata: null
        );
        await context.Send(new Uri("queue:notifications.sms.result"), processingDto);

        var sendResult = await smsSender.SendSmsAsync(sms);
        if (sendResult is SendSmsStatusEnum.Success)
        {
            var successDto = new BrokerResponseDto(
                Id: sms.Id,
                Status: StatusEnum.Sent.ToString().ToUpper(),
                ErrorMessage: null,
                Timestamp: DateTime.UtcNow,
                Metadata: null
            );
            await context.Send(new Uri("queue:notifications.sms.result"), successDto);
        }
        else
        {
            var failDto = new BrokerResponseDto(
                Id: sms.Id,
                Status: StatusEnum.Failed.ToString().ToUpper(),
                ErrorMessage: "Happened uknown error while sending SMS",
                Timestamp: DateTime.UtcNow,
                Metadata: null
            );
            await context.Send(new Uri("queue:notifications.sms.result"), failDto);
        }
    }
}
