using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedSpecies
{
    public static async Task SeedSpeciesData(AppDbContext context)
    {
        var speciesExist = await context.Species.AnyAsync();
        if (speciesExist) return;

        await context.Database.ExecuteSqlRawAsync(@"
            INSERT INTO Species (Name, Description, CreatedAt, IsDeleted) VALUES 
            ('Dog', 'Domesticated carnivorous mammal', GETDATE(), 0), 
            ('Cat', 'Small domesticated carnivorous mammal', GETDATE(), 0)"
        );
    }
}
