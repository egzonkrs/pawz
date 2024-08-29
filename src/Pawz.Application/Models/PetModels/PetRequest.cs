using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Pawz.Application.Models.PetModels;

/// <summary>
/// Represents the data required to create or update a Pet entity.
/// This class is used to capture the input data from the client or user interface.
/// </summary>
public class PetRequest
{
    /// <summary>
    /// The Id of the pet
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the pet
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The Id of the species of the pet
    /// </summary>
    public int SpeciesId { get; set; }

    /// <summary>
    /// The species of the pet
    /// </summary>
    public Species Species { get; set; }

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
    public int AgeYears { get; set; }

    /// <summary>
    /// The age of the pet in months
    /// </summary>
    public int AgeMonths { get; set; }

    /// <summary>
    /// Additional information about the pet
    /// </summary>
    public string About { get; set; }

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
    public ICollection<PetImage> PetImages { get; set; }

    /// <summary>
    /// Adoption requests for the pet
    /// </summary>
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();

    /// <summary>
    /// The ID of the user who posted the pet.
    /// </summary>
    public string PostedByUserId { get; set; }

    /// <summary>
    /// The user who made the request.
    /// </summary>
    public ApplicationUser User { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is soft-deleted.
    /// This property is implemented from the <see cref="ISoftDelete"/> interface.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the entity was soft-deleted.
    /// This property is implemented from the <see cref="ISoftDelete"/> interface.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}