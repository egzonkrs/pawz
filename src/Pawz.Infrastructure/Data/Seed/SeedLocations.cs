using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public static class SeedLocations
{
    public static async Task SeedLocationData(AppDbContext context)
    {
        var locationsExist = await context.Locations.AnyAsync();
        if (locationsExist) return;

        var locations = new List<Location>
        {
            new Location
            {
                CityId = 1, // Pristina
                Address = "Bulevardi Nënë Tereza 1",
                PostalCode = "10000",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 2, // Prizren
                Address = "Rruga e Shadërvanit 15",
                PostalCode = "20000",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 3, // Mitrovica
                Address = "Rruga Adem Jashari 9",
                PostalCode = "40000",
                IsDeleted = false,
                DeletedAt = null
            },

            // Albania Locations
            new Location
            {
                CityId = 6, // Tirana
                Address = "Bulevardi Dëshmorët e Kombit 10",
                PostalCode = "1010",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 7, // Durres
                Address = "Rruga Taulantia 8",
                PostalCode = "2001",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 7, // Shkoder
                Address = "Rruga Kole Idromeno 5",
                PostalCode = "4001",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Locations.AddRange(locations);
        await context.SaveChangesAsync();
    }
}
