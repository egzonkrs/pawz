using Pawz.Domain.Specifications;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Interfaces;

namespace Pawz.Infrastructure.Data;

public class SpecificationEvaluator<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
