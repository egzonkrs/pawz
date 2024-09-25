using System.Collections.Generic;

namespace Pawz.Web.Models;

public class MyPetsViewModel<T>
{
    /// <summary>
    /// Gets or sets the collection of pets displayed on the current page.
    /// </summary>
    public IEnumerable<T> Pets { get; set; }

    /// <summary>
    /// Gets or sets the cursor for fetching the next set of pets in the paginated list.
    /// </summary>
    public string NextCursor { get; set; }

    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages available.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the number of pets displayed per page.
    /// </summary>
    public int PageSize { get; set; }
}

