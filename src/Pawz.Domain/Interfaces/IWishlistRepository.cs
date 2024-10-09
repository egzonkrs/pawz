using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist, int>
{
    Task<List<Wishlist>> GetWishlistForUserAsync(string userId);
    Task<Wishlist?> GetWishlistItemAsync(string userId, int petId);
}
