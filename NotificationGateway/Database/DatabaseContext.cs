using Microsoft.EntityFrameworkCore;
using NotificationGateway.Database.Models;

namespace NotificationGateway.Database;

public class DatabaseContext : DbContext
{
    public DbSet<NotificationModel> Notifications { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }
}
