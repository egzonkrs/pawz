using Microsoft.AspNetCore.Http;
using Pawz.Domain.Enums;
using System.Collections.Generic;

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
    public string AgeYears { get; set; }

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
    public int LocationId { get; set; }

    /// <summary>
    /// The Id of the city where the pet is located.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// The address where the pet is located.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The postal code of the location where the pet is located.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// The ID of the user who posted the pet.
    /// </summary>
    public string PostedByUserId { get; set; }

    /// <summary>
    /// Status of the pet (e.g., 'Pending', 'Approved', 'Rejected').
    /// </summary>
    public PetStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the collection of image files associated with the pet.
    /// These files will be uploaded and stored as part of the pet's profile.
    /// </summary>
    public IEnumerable<IFormFile> ImageFiles { get; set; }
}
