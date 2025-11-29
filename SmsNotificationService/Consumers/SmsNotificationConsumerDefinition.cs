using MassTransit;

namespace SmsNotificationService.Services;

public class SmsNotificationConsumerDefinition : ConsumerDefinition<SmsNotificationConsumer>
{
    public SmsNotificationConsumerDefinition()
    {
        EndpointName = "notifications.sms.send";
    }
}
