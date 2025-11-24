using NotificationGateway.Enums;

namespace NotificationGateway.Database.Models;

public class NotificationModel
{
    public Guid Id { get; set; }
    public StatusEnum Status { get; set; }

    public required NotificationTypeEnum Type { get; set; }
    public required string Recipient { get; set; }
    public required string Message { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
    public DateTime CreatedAt { get; set; }
}
