using Pawz.Domain.Enums;
using System;

namespace Pawz.Web.Models.NotificationModels;

/// <summary>
/// Represents the data required to create a notification.
/// </summary>
public class NotificationRequestViewModel
{
    /// <summary>
    /// Gets or sets the ID of the recipient of the notification.
    /// </summary>
    public string? RecipientId { get; set; }

    /// <summary>
    /// Gets or sets the message content of the notification.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the ID of the pet associated with the notification, if applicable.
    /// </summary>
    public int? PetId { get; set; }

    /// <summary>
    /// Gets or sets the name of the pet associated with the notification, if applicable.
    /// </summary>
    public string? PetName { get; set; }

    /// <summary>
    /// Gets or sets the ID of the sender of the notification.
    /// </summary>
    public string? SenderId { get; set; }

    /// <summary>
    /// Gets or sets the name of the sender of the notification.
    /// </summary>
    public string? SenderName { get; set; }

    /// <summary>
    /// Gets or sets the type of the notification.
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the notification.
    /// </summary>
    public DateTime? CreatedAt { get; set; }
}
