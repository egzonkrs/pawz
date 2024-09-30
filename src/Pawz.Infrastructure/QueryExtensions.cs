using Microsoft.EntityFrameworkCore;
using Pawz.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Pawz.Infrastructure;

public static class QueryExtensions
{
    public static async Task<QueryParams<TResponse>> ApplyQueryParamsAsync<TEntity, TResponse>(
        this IQueryable<TEntity> query,
        QueryParams<TResponse> queryParams,
        Func<IEnumerable<TEntity>, IEnumerable<TResponse>> mapToResponse,
        string[] searchProperties,
        CancellationToken cancellationToken = default)
    {
        // Apply Searching
        if (!string.IsNullOrEmpty(queryParams.SearchQuery) && searchProperties.Length > 0)
        {
            var searchQuery = string.Join(" OR ", searchProperties.Select(p => $"{p}.Contains(@0)"));
            query = query.Where(searchQuery, queryParams.SearchQuery);
        }

        // Apply Filtering
        if (!string.IsNullOrEmpty(queryParams.FilterBy) && !string.IsNullOrEmpty(queryParams.FilterValue))
        {
            query = query.Where($"{queryParams.FilterBy}.Contains(@0)", queryParams.FilterValue);
        }

        // Apply Sorting using Dynamic LINQ
        if (!string.IsNullOrEmpty(queryParams.SortBy))
        {
            var sortDirection = queryParams.SortDescending ? "descending" : "ascending";
            query = query.OrderBy($"{queryParams.SortBy} {sortDirection}");
        }

        // Apply Pagination
        queryParams.TotalCount = await query.CountAsync(cancellationToken);
        queryParams.TotalPages = (int)Math.Ceiling(queryParams.TotalCount / (double)queryParams.PageSize);

        // Ensure the current page is valid (if the page number is out of range, adjust it)
        queryParams.CurrentPage = queryParams.CurrentPage > queryParams.TotalPages
                                  ? queryParams.TotalPages
                                  : queryParams.CurrentPage;

        if (queryParams.CurrentPage < 1)
        {
            queryParams.CurrentPage = 1;
        }

        // Skip and Take for pagination
        var items = await query.Skip((queryParams.CurrentPage - 1) * queryParams.PageSize)
                               .Take(queryParams.PageSize)
                               .ToListAsync(cancellationToken);

        // Map the items to the response type
        queryParams.Items = mapToResponse(items);

        return queryParams;
    }
}

