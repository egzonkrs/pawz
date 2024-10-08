namespace Pawz.Domain.Helpers;

public sealed class QueryParams
{
    public string FilterBy { get; set; }
    public string FilterValue { get; set; }
    public string SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
    public string SearchQuery { get; set; }
    public string Breed { get; set; }
    public string Location { get; set; }
    public int PageNumber { get; set; } = 1;
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
}
