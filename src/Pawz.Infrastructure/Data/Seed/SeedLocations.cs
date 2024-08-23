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
            SET IDENTITY_INSERT Locations ON;

            INSERT INTO Locations (Id, City, State, Country, PostalCode, IsDeleted, DeletedAt) VALUES
            (1, 'New York', 'NY', 'USA', '10001', 0, NULL),
            (2, 'Los Angeles', 'CA', 'USA', '90001', 0, NULL),
            (3, 'Chicago', 'IL', 'USA', '60601', 0, NULL),
            (4, 'Houston', 'TX', 'USA', '77001', 0, NULL),
            (5, 'Toronto', 'ON', 'Canada', 'M5H 2N2', 0, NULL);

            SET IDENTITY_INSERT Locations OFF;"
        );
    }
}
