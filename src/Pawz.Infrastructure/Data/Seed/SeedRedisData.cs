using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Interfaces;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public static class SeedRedisData
{
    public static async Task SeedWishlistsToRedis(AppDbContext context, IRedisRepository redisRepository)
    {
        var anyWishlistsInRedis = await redisRepository.HasKeyAsync("wishlist:*");

        if (anyWishlistsInRedis) return;

        var wishlists = await context.Wishlists
            .Include(w => w.Pets)
            .ToListAsync();

        if (wishlists.Count == 0) return;

        foreach (var wishlist in wishlists)
        {
            await redisRepository.SetDataAsync($"wishlist:{wishlist.UserId}", wishlist);
        }
    }
}
