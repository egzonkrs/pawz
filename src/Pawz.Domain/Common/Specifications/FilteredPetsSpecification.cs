using Pawz.Domain.Entities;
using Pawz.Domain.Helpers;

namespace Pawz.Domain.Common.Specifications;

public class FilteredPetsSpecification : Specification<Pet>
{
    public FilteredPetsSpecification(string? speciesName, string? breedName)
    {
        var baseSpecification = new PetWithRelatedEntitiesSpecification();

        foreach (var include in baseSpecification.Includes)
        {
            AddInclude(include);
        }

        foreach (var includeString in baseSpecification.IncludeStrings)
        {
            AddInclude(includeString);
        }

        UseSplitQuery();

        if (!string.IsNullOrEmpty(speciesName))
        {
            Criteria = p => p.Breed.Species.Name == speciesName;
        }

        if (!string.IsNullOrEmpty(breedName))
        {
            Criteria = Criteria != null
                ? Criteria.And(p => p.Breed.Name == breedName)
                : p => p.Breed.Name == breedName;
        }
    }
}
