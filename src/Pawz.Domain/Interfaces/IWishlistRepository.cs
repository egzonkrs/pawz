using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist, int>
{
    Task<Wishlist?> GetWishlistForUserAsync(string userId, CancellationToken cancellationToken = default);
}
