using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotificationGateway.Database.Models;

namespace NotificationGateway.Database;

public class DatabaseContext : DbContext
{
    public DbSet<NotificationModel> Notifications { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
