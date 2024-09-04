using Pawz.Application.Models.BreedModels;
using Pawz.Application.Models.PetModels;
using System;
using System.Collections.Generic;

namespace Pawz.Application.Models.SpeciesModels;
public class SpeciesResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<BreedResponse> Breeds { get; set; } = new List<BreedResponse>();
    public ICollection<PetResponse> Pets { get; set; } = new List<PetResponse>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}