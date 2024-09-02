using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Web.Models.Breed;
using Pawz.Web.Models.City;
using System.Collections.Generic;

namespace Pawz.Web.Models.Pet;

public class AdoptionRequestCreateModel
{
    /// <summary>
    /// Gets or sets the Country of the user that is requesting to adopt.
    /// </summary>
    public SelectList Countries { get; set; }

    /// <summary>
    /// Gets or sets the City of the user that is requesting to adopt.
    /// </summary>
    public SelectList Cities { get; set; }

    /// <summary>
    /// Gets or sets the Address of the user that is requesting to adopt.
    /// </summary>
    public string Address { get; set; }

    public int CityId { get; set; }

    public int CountryId { get; set; }
    public List<CityViewModel> AllCities { get; set; }
}
