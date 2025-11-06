using NotificationGateway.Enums;

namespace NotificationGateway.Interfaces;

public interface INotificationHandlersFactory
{
    public INotificationHandler GetHandler(NotificationTypeEnum type);
    public bool IsSupported(NotificationTypeEnum type);
}
