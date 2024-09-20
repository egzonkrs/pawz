using System;

namespace Pawz.Application.Models.NotificationModels;

/// <summary>
/// Represents a response containing notification details.
/// </summary>
public class NotificationResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the notification.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the message content of the notification.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the ID of the sender of the notification.
    /// </summary>
    public string SenderId { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who is sending the notification.
    /// </summary>
    public string SenderName { get; set; }

    /// <summary>
    /// Gets or sets the ID of the recipient who received the notification.
    /// </summary>
    public string RecipientId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the notification has been read.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets the ID of the pet associated with the notification.
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// Gets or sets the name of the pet associated with the notification.
    /// </summary>
    public string PetName { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the notification was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
