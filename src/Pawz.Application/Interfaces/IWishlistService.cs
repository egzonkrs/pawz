using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IWishlistService
{
    Task<Result<Wishlist>> GetWishlistForUserAsync();
    Task<Result<Wishlist>> AddPetToWishlistAsync(int petId);
    Task<Result<Wishlist>> RemovePetFromWishlistAsync(int petId);
    Task<Wishlist> SetWishlistAsync(Wishlist wishlist);
    Task<Wishlist> GetWishlistAsync(string key);
    Task<bool> DeleteWishlistAsync(string key);
}
