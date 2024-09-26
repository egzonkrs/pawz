using Pawz.Application.Models.Pet;
using System.Collections.Generic;

namespace Pawz.Application.Models.Pagination;
public class PaginatedUserPetsResponse
{
    /// <summary>
    /// A collection of UserPetResponse objects representing the pets of the user.
    /// </summary>
    public IEnumerable<UserPetResponse> Pets { get; set; }

    /// <summary>
    /// The total number of pets associated with the user.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// The current page number in the paginated result set.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The total number of pages available based on the page size.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The number of pets displayed per page.
    /// </summary>
    public int PageSize { get; set; }

}
