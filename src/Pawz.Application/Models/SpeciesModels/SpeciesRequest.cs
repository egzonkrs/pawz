using Pawz.Application.Models.BreedModels;
using Pawz.Application.Models.PetModels;
using System;
using System.Collections.Generic;

namespace Pawz.Application.Models.SpeciesModels;
public class SpeciesRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<BreedRequest> Breeds { get; set; } = new List<BreedRequest>();
    public ICollection<PetRequest> Pets { get; set; } = new List<PetRequest>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
