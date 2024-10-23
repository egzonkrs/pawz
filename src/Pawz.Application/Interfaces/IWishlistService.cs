using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IWishlistService
{
    Task<Result<Wishlist>> GetWishlistForUserAsync();
    Task<Result<Wishlist>> AddPetToWishlistAsync(int petId);
    Task<Result<Wishlist>> RemovePetFromWishlistAsync(int petId);
    Task<Result<bool>> DeleteWishlistAsync(string userId);
}
