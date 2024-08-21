using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Domain.Enums;
using System.Collections.Generic;

namespace Pawz.Web.Models;

public class PetCreateViewModel
{
    public string Name { get; set; }
    public int SpeciesId { get; set; }
    public IEnumerable<SelectListItem>? Species { get; set; }
    public int BreedId { get; set; }
    public IEnumerable<SelectListItem>? Breeds { get; set; }
    public int AgeYears { get; set; }
    public int AgeMonths { get; set; }
    public string About { get; set; }
    public decimal Price { get; set; }
    public int LocationId { get; set; }
    public IEnumerable<SelectListItem>? Locations { get; set; }
    public PetStatus Status { get; set; }
}
