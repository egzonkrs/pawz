using System;
using System.Collections.Generic;

namespace Pawz.Web.Models.Location;

public class CountryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CityViewModel> Cities { get; set; } = new List<CityViewModel>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
