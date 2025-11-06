using System.Text.Json.Serialization;

namespace NotificationGateway.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationTypeEnum
{
    Sms = 1,
    Email = 2,
}
