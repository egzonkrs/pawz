using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
    public class SeedBreeds
    {
        public static async Task SeedBreedData(AppDbContext context)
        {
            var breedsExists = await context.Breeds.AnyAsync();
            if (breedsExists) return;

            await context.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO Breeds (Id, SpeciesId, Name, Description, isDeleted) VALUES 
                    (1, 1, 'Golden Retriever', 'Friendly and tolerant breed.', false), 
                    (2, 1, 'Labrador Retriever', 'Outgoing and even-tempered breed.', false), 
                    (3, 2, 'Persian', 'Affectionate and quiet breed with long fur.', false), 
                    (4, 2, 'Siamese', 'Social and intelligent breed with striking blue eyes.', false)"
            );
        }
    }
}
