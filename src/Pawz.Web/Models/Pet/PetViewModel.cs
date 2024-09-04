using Pawz.Domain.Entities;
using Pawz.Web.Models.Breed;
using Pawz.Web.Models.PetImage;
using System;
using System.Collections.Generic;

namespace Pawz.Web.Models.Pet;

public class PetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string About { get; set; }
    public ICollection<PetImageViewModel> PetImages { get; set; } = new List<PetImageViewModel>();
    public BreedViewModel Breed { get; set; }
    public string LocationCity { get; set; }
    public int AgeYears { get; set; }
    public int AgeMonths { get; set; }
    public UserViewModel User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public decimal Price { get; set; }
}

public class UserViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<PetViewModel> Pets { get; set; }
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
