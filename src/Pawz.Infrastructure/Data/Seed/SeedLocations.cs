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
                CityId = 1,
                Address = "Bulevardi Nënë Tereza 1",
                PostalCode = "10000",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 1,
                Address = "Rruga Dardania 3",
                PostalCode = "12000",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 2,
                Address = "Rruga e Dajti 5",
                PostalCode = "1001",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Locations.AddRange(locations);
        await context.SaveChangesAsync();
    }
}
