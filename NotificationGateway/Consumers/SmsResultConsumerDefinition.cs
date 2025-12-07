using MassTransit;

namespace NotificationGateway.Consumers;

public class SmsResultConsumerDefinition : ConsumerDefinition<SmsResultConsumer>
{
    public SmsResultConsumerDefinition()
    {
        EndpointName = "notifications.sms.result";
    }
}
