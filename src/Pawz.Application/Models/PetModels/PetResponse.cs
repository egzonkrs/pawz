using Pawz.Application.Models.PetImagesModels;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Pawz.Application.Models.PetModels;

public class PetResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SpeciesId { get; set; }
    public Species Species { get; set; }
    public int BreedId { get; set; }
    public Breed Breed { get; set; }
    public string AgeYears { get; set; }
    public string About { get; set; }
    public decimal Price { get; set; }
    public PetStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<PetImageResponse> PetImages { get; set; } = new List<PetImageResponse>();
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
    public string PostedByUserId { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public bool HasExistingAdoptionRequest { get; set; }
    public int? AdoptionRequestId { get; set; }
}
