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
            SET IDENTITY_INSERT Species ON;
        
            INSERT INTO Species (Id, Name, Description, CreatedAt, IsDeleted) VALUES 
            (1, 'Dog', 'Domesticated carnivorous mammal', GETDATE(), 0), 
            (2, 'Cat', 'Small domesticated carnivorous mammal', GETDATE(), 0);
        
            SET IDENTITY_INSERT Species OFF;"
        );
    }
}
