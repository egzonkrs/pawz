using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pawz.Domain.Helpers;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T));

        var visitor = new ReplaceExpressionVisitor();
        visitor.Add(first.Parameters[0], parameter);
        visitor.Add(second.Parameters[0], parameter);

        var combined = visitor.Visit(Expression.AndAlso(first.Body, second.Body));

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<Expression, Expression> _replacements;

        public ReplaceExpressionVisitor()
        {
            _replacements = new Dictionary<Expression, Expression>();
        }

        public void Add(Expression from, Expression to)
        {
            _replacements[from] = to;
        }

        public override Expression Visit(Expression node)
        {
            if (node != null && _replacements.TryGetValue(node, out var replacement))
                return replacement;
            return base.Visit(node);
        }
    }
}

