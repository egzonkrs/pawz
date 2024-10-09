using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Pawz.Infrastructure.Repositories;

public class WishlistRepository : GenericRepository<Wishlist, int>, IWishlistRepository
{
    public WishlistRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Wishlist?> GetWishlistForUserAsync(string userId)
    {
        return await _dbSet
            .Include(w => w.Pets)
            .ThenInclude(p => p.Breed)
            .ThenInclude(b => b.Species)
            .FirstOrDefaultAsync(w => w.UserId == userId && !w.IsDeleted);
    }

    public async Task<Wishlist?> GetWishlistItemAsync(string userId, int petId)
    {
        return await _dbSet
            .Include(w => w.Pets)
            .FirstOrDefaultAsync(w => w.UserId == userId && !w.IsDeleted && w.Pets.Any(p => p.Id == petId));
    }
}
