using Pawz.Web.Models.Breed;
using Pawz.Web.Models.Location;
using Pawz.Web.Models.PetImage;
using System;
using System.Collections.Generic;

namespace Pawz.Web.Models.Pet;

public class PetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string About { get; set; }
    public List<PetImageViewModel> PetImages { get; set; } = new List<PetImageViewModel>();
    public BreedViewModel Breed { get; set; }
    public LocationViewModel Location { get; set; }
    public string AgeYears { get; set; }
    public UserViewModel User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public decimal Price { get; set; }
    public AdoptionRequestCreateModel AdoptionRequestCreateModel { get; set; }
    public Pawz.Domain.Entities.Pet Pet { get; set; }
    public bool HasExistingAdoptionRequest { get; set; }
}
