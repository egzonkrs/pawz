using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist, string>
{
    Task<Wishlist> GetWishlistForUserAsync(string userId);
}
