using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
    public class SeedSpecies
    {
        public static async Task SeedSpeciesData(AppDbContext context)
        {
            var speciesExist = await context.Species.AnyAsync();
            if (speciesExist) return;

            await context.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO Species (Id, Name, Description, CreatedAt, isDeleted) VALUES 
                    (1, 'Dog', 'Domesticated carnivorous mammal', datetime('now'), false), 
                    (2, 'Cat', 'Small domesticated carnivorous mammal', datetime('now'), false)"
            );
        }
    }
}