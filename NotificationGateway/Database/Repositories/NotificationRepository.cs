using Microsoft.EntityFrameworkCore;
using NotificationGateway.Database.Models;
using NotificationGateway.Enums;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Database.Repositories;

public class NotificationRepository(DatabaseContext database) : INotificationRepository
{
    public async Task<NotificationModel> GetAsync(Guid notificationId)
    {
        var notification = await database.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);
        if (notification is null)
            throw new ArgumentNullException($"Got null for id {notificationId} while trying to get notification");

        return notification;
    }

    public async Task CreateAsync(NotificationModel notification)
    {
        database.Notifications.Add(notification);
        await database.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(Guid notificationId, StatusEnum status)
    {
        var notification = await database.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);
        if (notification is null)
            throw new ArgumentNullException($"Got null for id {notificationId} while updating status");

        notification.Status = status;
        notification.LastUpdatedAt = DateTime.UtcNow;
        await database.SaveChangesAsync();
    }

    public async Task<int> GetAmountOfRetries(Guid notificationId)
    {
        var notification = await database.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);
        if (notification is null)
            throw new ArgumentNullException($"Got null for id {notificationId} while getting amount of retries");

        return notification.AmountOfRetries;
    }

    public async Task IncrementAmountOfRetries(Guid notificationId)
    {
        var notification = await database.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);
        if (notification is null)
            throw new ArgumentNullException($"Got null for id {notificationId} while increasing amount of retries");

        notification.AmountOfRetries++;
        notification.LastUpdatedAt = DateTime.UtcNow;
        await database.SaveChangesAsync();
    }
}
