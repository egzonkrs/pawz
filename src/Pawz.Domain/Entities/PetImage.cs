using System;

namespace Pawz.Domain.Entities;

public class PetImage
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
    /// The URL of the pet image
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Description of the pet image
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Indicates if the image is the primary one for the pet
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// The date and time when the image was uploaded
    /// </summary>
    public DateTime UploadedAt { get; set; }

    /// <summary>
    /// The pet associated with this image
    /// </summary>
    public Pet Pets { get; set; }
}
