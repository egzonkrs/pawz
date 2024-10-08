namespace Pawz.Domain.Helpers;

/// <summary>
/// Parameters for querying, filtering, sorting, and pagination.
/// </summary>
public class QueryParams
{
    /// <summary>
    /// The property by which to filter the results (e.g., "name", "breed", "species").
    /// </summary>
    public string? FilterBy { get; set; }

    /// <summary>
    /// The value used to filter the results for the specified FilterBy property.
    /// </summary>
    public string? FilterValue { get; set; }

    /// <summary>
    /// The property by which to sort the results (e.g., "name", "breed", "species").
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Indicates whether the results should be sorted in descending order. Defaults to false (ascending).
    /// </summary>
    public bool SortDescending { get; set; } = false;

    /// <summary>
    /// The search query to filter the results by matching specific fields.
    /// </summary>
    public string? SearchQuery { get; set; }

    /// <summary>
    /// The search query to filter the results by matching specific fields.
    /// </summary>
    public string[]? SearchProperties { get; set; } = ["name"];

    /// <summary>
    /// The current page number for pagination. Defaults to 1.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// The total number of items returned by the query.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// The current page number being viewed in pagination.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The total number of pages available based on the query and PageSize.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The number of items per page for pagination.
    /// </summary>
    public int PageSize { get; set; }
}
