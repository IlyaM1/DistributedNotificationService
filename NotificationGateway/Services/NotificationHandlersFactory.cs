using NotificationGateway.Enums;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Services;

public class NotificationHandlersFactory(IEnumerable<INotificationHandler> handlers) : INotificationHandlersFactory
{
    private readonly Dictionary<NotificationTypeEnum, INotificationHandler> _handlers = handlers.ToDictionary(x => x.Type, x => x);

    public INotificationHandler GetHandler(NotificationTypeEnum type) => _handlers[type];
    public bool IsSupported(NotificationTypeEnum type) => _handlers.ContainsKey(type);
}
