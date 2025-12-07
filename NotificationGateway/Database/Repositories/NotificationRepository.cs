using Microsoft.EntityFrameworkCore;
using NotificationGateway.Database.Models;
using NotificationGateway.Enums;
using NotificationGateway.Interfaces;

namespace NotificationGateway.Database.Repositories;

public class NotificationRepository(DatabaseContext database) : INotificationRepository
{
    public async Task CreateAsync(NotificationModel notification)
    {
        database.Notifications.Add(notification);
        await database.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(Guid notificationId, StatusEnum status)
    {
        var notification = await database.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);
        if (notification is null)
            return;

        notification.Status = status;
        await database.SaveChangesAsync();
    }
}
