namespace Pawz.Application.Models.NotificationModels;

/// <summary>
/// Represents a request to update the status of a notification.
/// </summary>
public class NotificationUpdateRequest
{
    /// <summary>
    /// Gets or sets a value indicating whether the notification has been marked as read.
    /// </summary>
    public bool IsRead { get; set; }
}
