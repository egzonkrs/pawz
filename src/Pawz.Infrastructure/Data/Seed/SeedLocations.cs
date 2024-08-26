using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
    public class SeedLocations
    {
        public static async Task SeedLocationData(AppDbContext context)
        {
            var locationsExists = await context.Locations.AnyAsync();
            if (locationsExists) return;

            await context.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO Locations (Id, City, State, Country, PostalCode, isDeleted) VALUES
                    (1, 'New York', 'NY', 'USA', '10001', false),
                    (2, 'Los Angeles', 'CA', 'USA', '90001', false),
                    (3, 'Chicago', 'IL', 'USA', '60601', false),
                    (4, 'Houston', 'TX', 'USA', '77001', false),
                    (5, 'Toronto', 'ON', 'Canada', 'M5H 2N2', false)"
            );
        }
    }
}