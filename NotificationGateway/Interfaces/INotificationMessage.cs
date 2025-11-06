namespace NotificationGateway.Interfaces;

public interface INotificationMessage
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }

    public string QueueName { get; }
}
