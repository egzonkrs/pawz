using Pawz.Domain.Enums;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Pet : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// The Id of the pet
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the pet
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The Id of the breed of the pet
    /// </summary>
    public int BreedId { get; set; }

    /// <summary>
    /// The breed of the pet
    /// </summary>
    public Breed Breed { get; set; }

    /// <summary>
    /// The age of the pet in years
    /// </summary>
    public required string AgeYears { get; set; }

    /// <summary>
    /// Additional information about the pet
    /// </summary>
    public required string About { get; set; }

    /// <summary>
    /// The price of the pet
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Status of the pet (e.g., 'Pending', 'Approved', 'Rejected')
    /// </summary>
    public PetStatus Status { get; set; }

    /// <summary>
    /// The date and time when the pet was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The Id of the location where the pet is located
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// The location where the pet is located
    /// </summary>
    public Location Location { get; set; }

    /// <summary>
    /// Images of the pet
    /// </summary>
    public ICollection<PetImage> PetImages { get; set; } = new List<PetImage>();

    /// <summary>
    /// Adoption requests for the pet
    /// </summary>
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();

    public ICollection<Wishlist> WishlistedByUsers { get; set; }

    /// <summary>
    /// The ID of the user who posted the pet.
    /// </summary>
    public required string PostedByUserId { get; set; }

    /// <summary>
    /// The user who made the request.
    /// </summary>
    public ApplicationUser User { get; set; }

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
