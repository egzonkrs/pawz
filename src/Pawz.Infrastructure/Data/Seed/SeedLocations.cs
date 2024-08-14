using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
    public class SeedLocations
    {
        public static async Task SeedLocationData(AppDbContext context)
        {
            var locationsDoNotExist = await context.Locations.AnyAsync() is false;

            if (locationsDoNotExist)
            {
                await context.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO Locations (Id, City, State, Country, PostalCode) VALUES
                    (1, 'New York', 'NY', 'USA', '10001'),
                    (2, 'Los Angeles', 'CA', 'USA', '90001'),
                    (3, 'Chicago', 'IL', 'USA', '60601'),
                    (4, 'Houston', 'TX', 'USA', '77001'),
                    (5, 'Toronto', 'ON', 'Canada', 'M5H 2N2')"
                );

                await context.SaveChangesAsync();
            }
        }
    }
}