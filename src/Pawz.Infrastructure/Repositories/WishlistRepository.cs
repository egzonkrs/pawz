using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;

namespace Pawz.Infrastructure.Repositories;

public class WishlistRepository : GenericRepository<Wishlist, int>, IWishlistRepository
{
    public WishlistRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Wishlist?> GetWishlistForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(w => w.UserId == userId && !w.IsDeleted)
            .Include(w => w.Pets)
            .ThenInclude(p => p.PetImages)
            .Include(w => w.Pets)
            .ThenInclude(p => p.Location)
            .ThenInclude(l => l.City)
            .ThenInclude(c => c.Country)
            .FirstOrDefaultAsync();
    }
}
