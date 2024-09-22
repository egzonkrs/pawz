using Pawz.Domain.Interfaces;
using System;

namespace Pawz.Domain.Entities;

/// <summary>
/// Represents a notification entity in the system.
/// </summary>
public class Notification : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// Gets or sets the unique identifier of the notification.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the message content of the notification.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the notification.
    /// </summary>
    public string SenderId { get; set; }

    /// <summary>
    /// Gets or sets the sender's user information.
    /// </summary>
    public ApplicationUser? Sender { get; set; }

    /// <summary>
    /// Gets or sets the user who recives the notification.
    /// </summary>
    public string RecipientId { get; set; }

    /// <summary>
    /// Gets or sets the user who recives the notification.
    /// </summary>
    public ApplicationUser? Recipient { get; set; }

    /// <summary>
    /// Gets or sets the ID of the pet associated with the notification.
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// Gets or sets the pet associated with the notification.
    /// </summary>
    public Pet? Pet { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the notification has been marked as read.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the notification was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Indicates whether the entity is soft-deleted.
    /// This property is implemented from the <see cref="ISoftDeletion"/> interface.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The timestamp of when the entity was soft-deleted.
    /// This property is implemented from the <see cref="ISoftDeletion"/> interface.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}
