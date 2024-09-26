using Pawz.Domain.Entities;

namespace Pawz.Domain.Common.Specifications;

public class PetWithRelatedEntitiesSpecification : Specification<Pet>
{
    public PetWithRelatedEntitiesSpecification()
    {
        UseSplitQuery();
        AddInclude(p => p.PetImages);
        AddInclude(p => p.Breed);
        AddInclude(p => p.Breed.Species);
        AddInclude(p => p.User);
        AddInclude(p => p.Location);
        AddInclude(p => p.Location.City);
        AddInclude(p => p.Location.City.Country);
    }
}
