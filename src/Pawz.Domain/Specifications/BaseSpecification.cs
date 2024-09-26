using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pawz.Domain.Specifications;

/// <summary>
/// Provides a base specification class for applying filtering, searching, sorting, and pagination to a queryable collection of entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity that this specification applies to.</typeparam>
public class BaseSpecification<TEntity> where TEntity : Pet
{
    // Filters and Search
    protected Expression<Func<TEntity, bool>> Criteria { get; private set; }
    protected string SearchTerm { get; private set; }

    // Pagination
    protected int Skip { get; private set; }
    protected int Take { get; private set; }
    protected bool IsPagingEnabled { get; private set; }

    // Sorting
    protected Expression<Func<TEntity, object>> OrderBy { get; private set; }
    protected Expression<Func<TEntity, object>> OrderByDescending { get; private set; }


    /// <summary>
    /// Method to set the specification parameters
    /// </summary>
    /// <param name="criteria"></param>
    public void ApplyCriteria(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="searchTerm"></param>
    public void ApplySearch(string searchTerm)
    {
        SearchTerm = searchTerm;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    public void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="orderByExpression"></param>
    public void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="orderByDescExpression"></param>
    public void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    /// <summary>
    /// Method to apply the specification to a query.
    /// </summary>
    public IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> query)
    {
        if (Criteria is not null)
        {
            query = query.Where(Criteria);
        }

        // TODO: check how we can make it work with different entities without using reflection, so we can specify the property
        if (string.IsNullOrEmpty(SearchTerm) is false)
        {
            query = query.Where(p => p.Name.Contains(SearchTerm));
        }

        if (OrderBy is not null)
        {
            query = query.OrderBy(OrderBy);
        }

        if (OrderByDescending is not null)
        {
            query = query.OrderByDescending(OrderByDescending);
        }

        if (IsPagingEnabled)
        {
            query = query
                .Skip(Skip)
                .Take(Take);
        }

        return query;
    }
}


