using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IWishlistService
{
    Task<Result<List<Wishlist>>> AddPetToWishlistAsync(string userId, int petId);
    Task<Result<List<Wishlist>>> RemovePetFromWishlistAsync(string userId, int petId);
}
