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
            .Where(w => w.UserId == userId && !w.IsDeleted)
            .Select(w => new Wishlist
            {
                Id = w.Id,
                UserId = w.UserId,
                Pets = w.Pets.Select(p => new Pet
                {
                    Id = p.Id,
                    Name = p.Name,
                    AgeYears = p.AgeYears,  // Include the required AgeYears property
                    About = p.About,        // Include the required About property
                    PostedByUserId = p.PostedByUserId,  // Include the required PostedByUserId property
                    Breed = new Breed
                    {
                        Name = p.Breed.Name,
                        Species = new Species
                        {
                            Name = p.Breed.Species.Name,
                            Description = p.Breed.Species.Description
                        }
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Wishlist?> GetWishlistItemAsync(string userId, int petId)
    {
        return await _dbSet
            .Include(w => w.Pets)
            .FirstOrDefaultAsync(w => w.UserId == userId && !w.IsDeleted && w.Pets.Any(p => p.Id == petId));
    }
}
