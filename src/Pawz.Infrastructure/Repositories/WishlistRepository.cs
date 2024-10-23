using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;

namespace Pawz.Infrastructure.Repositories;

public class WishlistRepository : GenericRepository<Wishlist, string>, IWishlistRepository
{
    public WishlistRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Wishlist?> GetWishlistForUserAsync(string userId, CancellationToken cancellationToken = default)
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
                    AgeYears = p.AgeYears,
                    About = p.About,
                    PostedByUserId = p.PostedByUserId,
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
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Wishlist?> GetWishlistPetsWithRelatedEntities(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(w => w.Pets)
            .ThenInclude(p => p.Breed)
            .ThenInclude(p => p.Species)
            .FirstOrDefaultAsync(w => w.UserId == userId && !w.IsDeleted, cancellationToken);
    }
}
