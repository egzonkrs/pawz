using System;
using System.Collections.Generic;

namespace Pawz.Domain.Specifications;

public class QueryParameters
{
    // Filtering
    public Dictionary<string, string> Filters { get; set; } = new(StringComparer.InvariantCultureIgnoreCase); // Case-insensitive dictionary

    // Searching
    public string? SearchTerm { get; set; }

    // Sorting
    public string? Sort { get; set; }

    // Pagination
    private const int MaxPageSize = 10;
    private int _pageSize = 5; // Default page size
    public int PageIndex { get; set; } = 1; // Default page index
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value; // Preventing page size to be greater than `MaxPageSize`
    }
}
