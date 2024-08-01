using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Pets
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SpeciesId { get; set; }
    public int BreedId { get; set; }
    public int AgeYears { get; set; }
    public int AgeMonths { get; set; }
    public string About { get; set; }
    public decimal Price { get; set; }
    /// <summary>
    /// Status can be : 'Pending', 'Approved', 'Rejected
    /// </summary>
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int PostedByUserId { get; set; }
    public int LocationId { get; set; }
    public Users Users { get; set; }
    public Locations Locations { get; set; }
    public Species Species { get; set; }
    public Breeds Breeds { get; set; }
    public ICollection<PetImages> PetsImages { get; set; }
    public ICollection<AdoptionRequests> AdoptionRequests { get; set; }
}
