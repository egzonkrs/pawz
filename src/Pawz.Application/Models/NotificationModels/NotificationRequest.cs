namespace Pawz.Application.Models.NotificationModels;

/// <summary>
/// Represents a request to create a notification.
/// </summary>
public class NotificationRequest
{
    /// <summary>
    /// Gets or sets the message content of the notification.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the ID of the sender who is creating the notification.
    /// </summary>
    public string SenderId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the recipient who will receive the notification.
    /// </summary>
    public string RecipientId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the pet associated with the notification.
    /// </summary>
    public int PetId { get; set; }
}
