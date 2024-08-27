using Pawz.Domain.Enums;
using System;

namespace Pawz.Application.Models.Pet;

/// <summary>
/// Represents the data returned to the client after performing an operation related to a Pet entity.
/// This class is used to encapsulate the output data sent to the client or user interface.
/// </summary>
public class PetResponse
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
    /// The species of the pet
    /// </summary>
    public string Species { get; set; }

    /// <summary>
    /// The breed of the pet
    /// </summary>
    public string Breed { get; set; }

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
    /// The location where the pet is located
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// The date and time when the pet was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Indicates whether the pet is soft-deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}