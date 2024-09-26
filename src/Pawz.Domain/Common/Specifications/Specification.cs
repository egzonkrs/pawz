using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pawz.Domain.Common.Specifications;

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>> Criteria { get; protected set; }
    public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();
    public bool IsSplitQuery { get; private set; } = false;

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void UseSplitQuery()
    {
        IsSplitQuery = true;
    }

    public Specification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Specification() { }
}
