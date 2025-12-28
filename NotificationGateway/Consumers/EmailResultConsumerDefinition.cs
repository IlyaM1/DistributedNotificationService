using MassTransit;

namespace NotificationGateway.Consumers;

public class EmailResultConsumerDefinition : ConsumerDefinition<EmailResultConsumer>
{
    public EmailResultConsumerDefinition()
    {
        EndpointName = "notifications.email.result";
    }
}