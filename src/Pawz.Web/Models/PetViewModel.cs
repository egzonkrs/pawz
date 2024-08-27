using System;

namespace Pawz.Web.Models;

public class PetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string About { get; set; }
    public string ImageUrl { get; set; }
    public string SpeciesName { get; set; }
    public string BreedName { get; set; }
    public string Location { get; set; }
    public int AgeYears { get; set; }
    public int AgeMonths { get; set; }
    public string PostedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Price { get; set; }
}
