using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Web.Models.City;
using System.Collections.Generic;

namespace Pawz.Web.Models.Pet;

public class AdoptionRequestCreateModel
{
    public int Id { get; set; }

    public int PetId { get; set; }

    public int CityId { get; set; }

    public int CountryId { get; set; }

    public string Address { get; set; }

    public string PostalCode { get; set; }

    public SelectList Countries { get; set; }

    public SelectList Cities { get; set; }

    public SelectList Locations { get; set; }

    public List<CityViewModel> AllCities { get; set; }
}
