using Microsoft.EntityFrameworkCore;
using Pawz.Application.Helpers;
using Pawz.Domain.Entities;
using System;
using System.Linq;

namespace Pawz.Infrastructure;

public static class QueryExtensions
{
    public static IQueryable<TEntity> ApplyQueryParams<TEntity>(this IQueryable<TEntity> query, QueryParams queryParams, string[] searchProperties)
        where TEntity : Pet
    {
        if (string.IsNullOrEmpty(queryParams.SearchQuery) is false && searchProperties.Length is not 0)
        {
            query = ApplySearchFilters(query, searchProperties, queryParams.SearchQuery);
        }

        if (string.IsNullOrEmpty(queryParams.FilterBy) is false && !string.IsNullOrEmpty(queryParams.FilterValue))
        {
            query = ApplyFilter(query, queryParams.FilterBy, queryParams.FilterValue);
        }

        if (string.IsNullOrEmpty(queryParams.SortBy) is false)
        {
            query = ApplySorting(query, queryParams.SortBy, queryParams.SortDescending);
        }

        queryParams.TotalPages = (int)Math.Ceiling(queryParams.TotalCount / (double)queryParams.PageSize);

        queryParams.CurrentPage = queryParams.CurrentPage > queryParams.TotalPages
            ? queryParams.TotalPages
            : queryParams.CurrentPage;

        if (queryParams.CurrentPage < 1)
        {
            queryParams.CurrentPage = 1;
        }

        var queryable = query
            .Skip((queryParams.CurrentPage - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize);

        return queryable;
    }

    private static IQueryable<TEntity> ApplySearchFilters<TEntity>(
        IQueryable<TEntity> query,
        string[] searchProperties,
        string searchQuery) where TEntity : Pet
    {
        foreach (var property in searchProperties)
        {
            query = property.ToLower() switch
            {
                "name" => query.Where(p => EF.Functions.Contains(p.Name, searchQuery)),
                "breed" => query.Where(p => EF.Functions.Contains(p.Breed.Name, searchQuery)),
                "species" => query.Where(p => EF.Functions.Contains(p.Breed.Species.Name, searchQuery)),
                _ => query
            };
        }

        return query;
    }

    private static IQueryable<TEntity> ApplyFilter<TEntity>(
        IQueryable<TEntity> query,
        string filterBy,
        string filterValue) where TEntity : Pet
    {
        return filterBy.ToLower() switch
        {
            "name" => query.Where(p => EF.Functions.Contains(p.Name, filterValue)),
            "breed" => query.Where(p => EF.Functions.Contains(p.Breed.Name, filterValue)),
            "species" => query.Where(p => EF.Functions.Contains(p.Breed.Species.Name, filterValue)),
            _ => query
        };
    }

    private static IQueryable<TEntity> ApplySorting<TEntity>(
        IQueryable<TEntity> query,
        string sortBy,
        bool sortDescending) where TEntity : Pet
    {
        return sortBy.ToLower() switch
        {
            "name" => sortDescending
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),
            "breed" => sortDescending
                ? query.OrderByDescending(p => p.Breed.Name)
                : query.OrderBy(p => p.Breed.Name),
            "species" => sortDescending
                ? query.OrderByDescending(p => p.Breed.Species.Name)
                : query.OrderBy(p => p.Breed.Species.Name),
            _ => query
        };
    }
}
