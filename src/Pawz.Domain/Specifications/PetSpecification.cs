using Pawz.Domain.Entities;
using System;

namespace Pawz.Domain.Specifications;

public class PetSpecification : BaseSpecification<Pet>
{
    public PetSpecification(QueryParameters specParams)
    {
        foreach (var filter in specParams.Filters)
        {
            switch (filter.Key)
            {
                case "breed":
                    if (string.IsNullOrEmpty(filter.Value) is false)
                    {
                        ApplyCriteria(p => p.Breed.Name == filter.Value);
                    }
                    break;

                case "species":
                    if (string.IsNullOrEmpty(filter.Value) is false)
                    {
                        ApplyCriteria(p => p.Breed.Species.Name.Equals(filter.Value, StringComparison.OrdinalIgnoreCase));
                    }
                    break;

                // Other cases for other filters as needed...
            }
        }

        if (string.IsNullOrEmpty(specParams.SearchTerm) is false)
        {
            ApplySearch(specParams.SearchTerm);
            ApplyCriteria(p => p.Name.Contains(specParams.SearchTerm));
        }

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        if (string.IsNullOrEmpty(specParams.Sort) is false)
        {
            switch (specParams.Sort)
            {
                case "name":
                    ApplyOrderBy(p => p.Name);
                    break;
                case "name_desc":
                    ApplyOrderByDescending(p => p.Name);
                    break;
                case "age_years":
                    ApplyOrderBy(p => p.AgeYears);
                    break;
                case "status":
                    ApplyOrderBy(p => p.Status);
                    break;

            }
        }
    }

}
