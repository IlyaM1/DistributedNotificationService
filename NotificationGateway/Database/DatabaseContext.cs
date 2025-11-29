using Microsoft.EntityFrameworkCore;
using NotificationGateway.Database.Models;

namespace NotificationGateway.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<NotificationModel> Notifications { get; set; }
}
