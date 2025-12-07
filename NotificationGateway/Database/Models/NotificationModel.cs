using NotificationGateway.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationGateway.Database.Models;

public class NotificationModel
{
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(30)")]
    public StatusEnum Status { get; set; }

    public required NotificationTypeEnum Type { get; set; }
    public required string Recipient { get; set; }
    public required string Message { get; set; }

    public int AmountOfRetries { get; set; } = 0;

    public Dictionary<string, string>? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
