using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Helpers;

namespace Pawz.Infrastructure.Common;

public static class PetQueryExtensions
{
    public static IQueryable<TEntity> ApplyQueryParams<TEntity>(this IQueryable<TEntity> query, QueryParams queryParams)
        where TEntity : Pet
    {
        var searchPropertiesAreNotNull = queryParams.SearchProperties is not null;
        var searchQueryIsNotEmpty = string.IsNullOrEmpty(queryParams.SearchQuery) is false;

        if (searchPropertiesAreNotNull && searchQueryIsNotEmpty && queryParams.SearchProperties?.Length > 0)
        {
            query = ApplySearchFilters(query, queryParams.SearchProperties, queryParams.SearchQuery!);
        }

        var filterByIsNotEmpty = string.IsNullOrEmpty(queryParams.FilterBy) is false;
        var filterVaultIsNotEmpty = string.IsNullOrEmpty(queryParams.FilterValue) is false;

        if (filterByIsNotEmpty && filterVaultIsNotEmpty)
        {
            query = ApplyFilter(query, queryParams.FilterBy!, queryParams.FilterValue!);
        }

        var sortByIsNotEmpty = string.IsNullOrEmpty(queryParams.SortBy) is false;

        if (sortByIsNotEmpty)
        {
            query = ApplySorting(query, queryParams.SortBy!, queryParams.SortDescending);
        }

        return query;
    }

    private static IQueryable<TEntity> ApplySearchFilters<TEntity>(IQueryable<TEntity> query, string[] searchProperties, string searchQuery)
        where TEntity : Pet
    {
        searchQuery = searchQuery.ToLower();
        return query.Where(p =>
            (searchProperties.Contains("name") && EF.Functions.Like(p.Name.ToLower(), $"%{searchQuery}%")) ||
            (searchProperties.Contains("breed") && EF.Functions.Like(p.Breed.Name.ToLower(), $"%{searchQuery}%")) ||
            (searchProperties.Contains("species") && EF.Functions.Like(p.Breed.Species.Name.ToLower(), $"%{searchQuery}%"))
        );
    }

    private static IQueryable<TEntity> ApplyFilter<TEntity>(IQueryable<TEntity> query, string filterBy, string filterValue) where TEntity : Pet
    {
        var loweredFilterValue = filterValue.ToLower();
        return filterBy.ToLower() switch
        {
            "name" => query.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{loweredFilterValue}%")),
            "breed" => query.Where(p => EF.Functions.Like(p.Breed.Name.ToLower(), $"%{loweredFilterValue}%")),
            "species" => query.Where(p => EF.Functions.Like(p.Breed.Species.Name.ToLower(), $"%{loweredFilterValue}%")),
            _ => query
        };
    }

    private static IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> query, string sortBy, bool sortDescending) where TEntity : Pet
    {
        return sortDescending
            ? query.OrderByDescending(GetSortProperty<TEntity>(sortBy))
            : query.OrderBy(GetSortProperty<TEntity>(sortBy));
    }

    private static Expression<Func<TEntity, object>> GetSortProperty<TEntity>(string sortBy) where TEntity : Pet
    {
        return sortBy.ToLower() switch
        {
            "name" => pet => pet.Name,
            "breed" => pet => pet.Breed.Name,
            "species" => pet => pet.Breed.Species.Name,
            _ => pet => pet.Id
        };
    }
}
