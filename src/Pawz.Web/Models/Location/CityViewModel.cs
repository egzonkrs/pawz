using System;
using System.Collections.Generic;

namespace Pawz.Web.Models.Location;
public class CityViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public CountryViewModel Country { get; set; }
    public ICollection<LocationViewModel> Locations { get; set; } = new List<LocationViewModel>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}