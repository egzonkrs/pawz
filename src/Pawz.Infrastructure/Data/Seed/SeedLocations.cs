using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedLocations
{
    public static async Task SeedLocationData(AppDbContext context)
    {
        var locationsExists = await context.Locations.AnyAsync();
        if (locationsExists) return;

        await context.Database.ExecuteSqlRawAsync(@"
            INSERT INTO Locations (City, State, Country, PostalCode, IsDeleted, DeletedAt) VALUES
            ('New York', 'NY', 'USA', '10001', 0, NULL),
            ('Los Angeles', 'CA', 'USA', '90001', 0, NULL),
            ('Chicago', 'IL', 'USA', '60601', 0, NULL),
            ('Houston', 'TX', 'USA', '77001', 0, NULL),
            ('Toronto', 'ON', 'Canada', 'M5H 2N2', 0, NULL)"
        );
    }
}
