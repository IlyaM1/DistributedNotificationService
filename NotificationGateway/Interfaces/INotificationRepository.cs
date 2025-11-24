using NotificationGateway.Database.Models;
using NotificationGateway.Enums;

namespace NotificationGateway.Interfaces;

public interface INotificationRepository
{
    public Task<Guid> CreateAsync(NotificationModel notification);
    public Task UpdateStatusAsync(Guid notificationId, StatusEnum status);
}
