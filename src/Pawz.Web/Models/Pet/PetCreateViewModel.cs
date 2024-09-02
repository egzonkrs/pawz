using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Web.Models.Breed;
using Pawz.Web.Models.City;
using System.Collections.Generic;

namespace Pawz.Web.Models.Pet;

public class PetCreateViewModel
{
    /// <summary>
    /// The Id of the pet.
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
    /// The Id of the species of the pet.
    /// </summary>
    public int SpeciesId { get; set; }

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
    /// The Id of the city where the pet is located.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// The Id of the country where the pet is located.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// The address where the pet is located.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The postal code of the location where the pet is located.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// A list of breeds available for selection.
    /// </summary>
    public SelectList Breeds { get; set; }

    /// <summary>
    /// A list of species available for selection.
    /// </summary>
    public SelectList Species { get; set; }

    /// <summary>
    /// A list of countries available for selection.
    /// </summary>
    public SelectList Countries { get; set; }

    /// <summary>
    /// A list of cities available for selection.
    /// </summary>
    public SelectList Cities { get; set; }

    /// <summary>
    /// A list of locations available for selection.
    /// </summary>
    public SelectList Locations { get; set; }

    /// <summary>
    /// A list of all available breeds.
    /// </summary>
    public List<BreedViewModel> AllBreeds { get; set; }

    /// <summary>
    /// A list of all available cities.
    /// </summary>
    public List<CityViewModel> AllCities { get; set; }
}
