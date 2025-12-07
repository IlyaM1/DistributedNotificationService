using NotificationGateway.Database.Models;
using NotificationGateway.Enums;

namespace NotificationGateway.Interfaces;

public interface INotificationRepository
{
    public Task<NotificationModel> GetAsync(Guid notificationId);
    public Task CreateAsync(NotificationModel notification);
    public Task UpdateStatusAsync(Guid notificationId, StatusEnum status);
    public Task<int> GetAmountOfRetries(Guid notificationId);
    public Task IncrementAmountOfRetries(Guid notificationId);
}
