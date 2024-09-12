using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Domain.Enums;
using Pawz.Web.Models.City;
using System.Collections.Generic;

namespace Pawz.Web.Models.Pet;

public class AdoptionRequestCreateModel
{
    /// <summary>
    /// Gets or sets the adoption request ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the pet ID associated with the adoption request.
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// Gets or sets the city ID for the adopter's address.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Gets or sets the country ID for the adopter's address.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets the adopter's address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the adopter's postal code.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the list of available countries for the adoption request.
    /// </summary>
    public SelectList Countries { get; set; }

    /// <summary>
    /// Gets or sets the list of available cities for the adoption request.
    /// </summary>
    public SelectList Cities { get; set; }

    /// <summary>
    /// Gets or sets the list of available locations for the adoption request.
    /// </summary>
    public SelectList Locations { get; set; }

    /// <summary>
    /// Gets or sets the list of all cities for the adoption request.
    /// </summary>
    public List<CityViewModel> AllCities { get; set; }

    /// <summary>
    /// Gets or sets whether the adopter lives in a rented property.
    /// </summary>
    public YesNoEnum IsRentedProperty { get; set; }

    /// <summary>
    /// Gets or sets whether the adopter has outdoor space.
    /// </summary>
    public YesNoEnum HasOutdoorSpace { get; set; }

    /// <summary>
    /// Gets or sets the details about the adopter's outdoor space, if applicable.
    /// </summary>
    public string? OutdoorSpaceDetails { get; set; }

    /// <summary>
    /// Gets or sets whether the adopter owns other pets.
    /// </summary>
    public YesNoEnum OwnsOtherPets { get; set; }

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
    //public string Email { get; set; }

    /// <summary>
    /// Gets or sets any additional message or comments provided by the adopter.
    /// </summary>
    public string? Message { get; set; }
}
