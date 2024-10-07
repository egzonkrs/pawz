using Pawz.Application.Models.Pet;
using System.Collections.Generic;

namespace Pawz.Application.Models;

public class PaginatedUserPetsResponse
{
    public IEnumerable<UserPetResponse> Pets { get; set; }

    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }
}
