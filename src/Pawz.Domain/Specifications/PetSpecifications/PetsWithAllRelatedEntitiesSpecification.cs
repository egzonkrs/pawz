using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;

namespace Pawz.Domain.Specifications.PetSpecifications;

public class PetsWithAllRelatedEntitiesSpecification : BaseSpecification<Pet>
{
    public PetsWithAllRelatedEntitiesSpecification(PetFilterQueryParams filterParams = null)
        : base(x =>
            (filterParams == null || (string.IsNullOrEmpty(filterParams.BreedName) || x.Breed.Name.ToLower() == filterParams.BreedName.ToLower())) &&
            (filterParams == null || (string.IsNullOrEmpty(filterParams.SpeciesName) || x.Breed.Species.Name.ToLower() == filterParams.SpeciesName.ToLower()))
        )
    {
        // Include related entities
        AddInclude(p => p.PetImages);
        AddInclude(p => p.Include(p => p.Breed)
            .ThenInclude(b => b.Species));
        AddInclude(p => p.User);
        AddInclude(p => p.Include(p => p.Location)
            .ThenInclude(l => l.City)
            .ThenInclude(c => c.Country));
        AddInclude(p => p.AdoptionRequests);
    }
}
