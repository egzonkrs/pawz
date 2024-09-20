using Pawz.Domain.Enums;
using System;

namespace Pawz.Application.Models;

public class AdoptionRequestCreateRequest
{
    /// <summary>
    /// The Id of the pet.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The status of the adoption request.
    /// </summary>
    public AdoptionRequestStatus Status { get; set; }

    /// <summary>
    /// The date when the adoption request is made.
    /// </summary>
    public DateTime RequestDate { get; set; }

    /// <summary>
    /// The Id of the pet associated with this adoption request.
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// The Id of the location 
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// The Id of the city 
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// The address 
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The postal code of the location 
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// The ID of the user who made the request.
    /// </summary>
    public string RequesterUserId { get; set; }

    /// <summary>
    /// Gets or sets whether the adopter lives in a rented property.
    /// </summary>
    public bool IsRentedProperty { get; set; }

    /// <summary>
    /// Gets or sets whether the adopter has outdoor space.
    /// </summary>
    public bool HasOutdoorSpace { get; set; }

    /// <summary>
    /// Gets or sets the details about the adopter's outdoor space, if applicable.
    /// </summary>
    public string? OutdoorSpaceDetails { get; set; }

    /// <summary>
    /// Gets or sets whether the adopter owns other pets.
    /// </summary>
    public bool OwnsOtherPets { get; set; }

    /// <summary>
    /// Gets or sets the details about the adopter's other pets, if applicable.
    /// </summary>
    public string? OtherPetsDetails { get; set; }

    /// <summary>
    /// Gets or sets the adopter's contact number.
    /// </summary>
    public string ContactNumber { get; set; }

    /// <summary>
    /// Gets or sets the adopter's email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets any additional message or comments provided by the adopter.
    /// </summary>
    public string? Message { get; set; }
}
