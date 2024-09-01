using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Domain.Enums;

namespace Pawz.Web.Models;

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

    public PetStatus Status { get; set; }

    public int LocationId { get; set; }

    public int CityId { get; set; }

    public int CountryId { get; set; }

    public string Address { get; set; }

    public string PostalCode { get; set; }

    public SelectList Breeds { get; set; }
    public SelectList Species { get; set; }
    public SelectList Countries { get; set; }
    public SelectList Cities { get; set; }
    public SelectList Locations { get; set; }
}
