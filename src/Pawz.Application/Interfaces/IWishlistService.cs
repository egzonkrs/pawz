using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IWishlistService
{
    Task<Wishlist?> GetWishlistAsync(string key);
    Task<Wishlist?> SetWishlistAsync(Wishlist wishlist);
    Task<bool> DeleteWishlistAsync(string key);
    Task<bool> TestRedisConnectionAsync();
}
