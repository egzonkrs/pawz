using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pawz.Domain.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public BaseSpecification() { }

    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } =
        new List<Expression<Func<T, object>>>();

    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeStrings { get; } =
        new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
    {
        IncludeStrings.Add(includeExpression);
    }
}
