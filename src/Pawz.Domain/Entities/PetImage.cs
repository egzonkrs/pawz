using Pawz.Domain.Interfaces;
using System;

namespace Pawz.Domain.Entities;

public class PetImage : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// The Id of the pet image
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Id of the pet associated with this image
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// The pet associated with this image
    /// </summary>
    public Pet Pet { get; set; }

    /// <summary>
    /// The URL of the pet image
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Indicates if the image is the primary one for the pet
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// The date and time when the image was uploaded
    /// </summary>
    public DateTime UploadedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is soft-deleted.
    /// This property is implemented from the <see cref="ISoftDeletion"/> interface.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the entity was soft-deleted.
    /// This property is implemented from the <see cref="ISoftDeletion"/> interface.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}
