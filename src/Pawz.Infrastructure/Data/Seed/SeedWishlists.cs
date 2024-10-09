using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Pawz.Infrastructure.Data.Seed;

public static class SeedWishlists
{
    public static async Task SeedWishlistData(AppDbContext context)
    {
        var wishlistsExist = await context.Wishlists.AnyAsync();
        if (wishlistsExist) return;

        var maverick = await context.Users.SingleOrDefaultAsync(u => u.Email == "asd@qwe.com");
        var bob = await context.Users.SingleOrDefaultAsync(u => u.Email == "bob@example.com");
        var john = await context.Users.SingleOrDefaultAsync(u => u.Email == "john@example.com");

        if (maverick == null || bob == null || john == null)
        {
            throw new Exception("One or more users not found. Make sure the users exist.");
        }

        var pets = await context.Pets.Take(10).ToListAsync();

        if (pets.Count < 9)
        {
            throw new Exception("Not enough pets in the database. Make sure there are at least 9 pets.");
        }

        var wishlists = new List<Wishlist>
        {
            new()
            {
                UserId = maverick.Id,
                Pets = pets.Take(3).ToList(),
                IsDeleted = false
            },
            new()
            {
                UserId = bob.Id,
                Pets = pets.Skip(3).Take(3).ToList(),
                IsDeleted = false
            },
            new()
            {
                UserId = john.Id,
                Pets = pets.Skip(6).Take(3).ToList(),
                IsDeleted = false
            }
        };

        context.Wishlists.AddRange(wishlists);
        await context.SaveChangesAsync();
    }
}

