using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pawz.Domain.Common.Specifications;

public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    public bool IsSplitQuery { get; }
}
