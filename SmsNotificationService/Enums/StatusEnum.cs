namespace SmsNotificationService.Enums;

public enum StatusEnum
{
    /// <summary>
    ///     API accepted notification
    /// </summary>
    Created = 1,

    /// <summary>
    ///     Published in message queue
    /// </summary>
    Queued = 2,

    /// <summary>
    ///     Consumer started processing
    /// </summary>
    Processing = 3,

    /// <summary>
    ///     Notification successfully sent
    /// </summary>
    Sent = 4,

    /// <summary>
    ///     Notification send is failed
    /// </summary>
    Failed = 5,

    /// <summary>
    ///     Retrying to send notification
    /// </summary>
    Retrying = 6,

    /// <summary>
    ///     Dead letter queue failure
    /// </summary>
    PermanentlyFailed = 7
}
