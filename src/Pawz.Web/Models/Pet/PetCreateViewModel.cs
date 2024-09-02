using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Web.Models.Breed;
using Pawz.Web.Models.City;
using System.Collections.Generic;

namespace Pawz.Web.Models.Pet;

public class PetCreateViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int BreedId { get; set; }

    public int SpeciesId { get; set; }

    public int AgeYears { get; set; }

    public int AgeMonths { get; set; }

    public string About { get; set; }

    public decimal Price { get; set; }

    public int CityId { get; set; }

    public int CountryId { get; set; }

    public string Address { get; set; }

    public string PostalCode { get; set; }

    public SelectList Breeds { get; set; }
    public SelectList Species { get; set; }
    public SelectList Countries { get; set; }
    public SelectList Cities { get; set; }
    public SelectList Locations { get; set; }

    public List<BreedViewModel> AllBreeds { get; set; }
    public List<CityViewModel> AllCities { get; set; }
}
