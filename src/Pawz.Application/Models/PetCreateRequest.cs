using Pawz.Domain.Enums;

namespace Pawz.Application.Models;

public class PetCreateRequest
{
    /// <summary>
    /// The Id of the pet
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// The name of the pet.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The Id of the breed of the pet.
    /// </summary>
    public int BreedId { get; set; }

    /// <summary>
    /// The age of the pet in years.
    /// </summary>
    public int AgeYears { get; set; }

    /// <summary>
    /// The age of the pet in months.
    /// </summary>
    public int AgeMonths { get; set; }

    /// <summary>
    /// Additional information about the pet.
    /// </summary>
    public string About { get; set; }

    /// <summary>
    /// The price of the pet.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The Id of the location where the pet is located.
    /// </summary>
    public int? LocationId { get; set; }

    /// <summary>
    /// The ID of the user who posted the pet.
    /// </summary>
    public string PostedByUserId { get; set; }

    /// <summary>
    /// Status of the pet (e.g., 'Pending', 'Approved', 'Rejected').
    /// </summary>
    public PetStatus Status { get; set; }
}
