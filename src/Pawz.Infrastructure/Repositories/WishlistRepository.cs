using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Pawz.Infrastructure.Repositories;

public class WishlistRepository : GenericRepository<Wishlist, int>, IWishlistRepository
{
    public WishlistRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Wishlist>> GetWishlistForUserAsync(string userId)
    {
        return await _dbSet
            .Where(w => w.UserId == userId && !w.IsDeleted)
            .ToListAsync();
    }

    public async Task<Wishlist?> GetWishlistItemAsync(string userId, int petId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(w => w.UserId == userId && w.PetId == petId && !w.IsDeleted);
    }
}
