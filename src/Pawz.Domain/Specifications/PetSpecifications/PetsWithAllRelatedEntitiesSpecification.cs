using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;

namespace Pawz.Domain.Specifications.PetSpecifications;

public class PetsWithAllRelatedEntitiesSpecification : BaseSpecification<Pet>
{
    public PetsWithAllRelatedEntitiesSpecification()
    {
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
