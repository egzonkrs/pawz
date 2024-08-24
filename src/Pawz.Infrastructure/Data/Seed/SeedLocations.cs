using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedLocations
{
    public static async Task SeedLocationData(AppDbContext context)
    {
        var locationsExist = await context.Locations.AnyAsync();
        if (locationsExist) return;

        var locations = new List<Location>
        {
            new Location
            {
                City = "New York",
                State = "NY",
                Country = "USA",
                PostalCode = "10001",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                City = "Los Angeles",
                State = "CA",
                Country = "USA",
                PostalCode = "90001",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                City = "Chicago",
                State = "IL",
                Country = "USA",
                PostalCode = "60601",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                City = "Houston",
                State = "TX",
                Country = "USA",
                PostalCode = "77001",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                City = "Toronto",
                State = "ON",
                Country = "Canada",
                PostalCode = "M5H 2N2",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Locations.AddRange(locations);
        await context.SaveChangesAsync();
    }
}
