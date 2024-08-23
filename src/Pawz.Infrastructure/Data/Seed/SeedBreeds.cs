using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedBreeds
{
    public static async Task SeedBreedData(AppDbContext context)
    {
        var breedsExists = await context.Breeds.AnyAsync();
        if (breedsExists) return;

        await context.Database.ExecuteSqlRawAsync(@"
            SET IDENTITY_INSERT Breeds ON;

            INSERT INTO Breeds (Id, SpeciesId, Name, Description, IsDeleted, DeletedAt) VALUES
            (1, 1, 'Golden Retriever', 'Friendly and tolerant breed.', 0, NULL),
            (2, 1, 'Labrador Retriever', 'Outgoing and even-tempered breed.', 0, NULL),
            (3, 2, 'Persian', 'Affectionate and quiet breed with long fur.', 0, NULL),
            (4, 2, 'Siamese', 'Social and intelligent breed with striking blue eyes.', 0, NULL);

            SET IDENTITY_INSERT Breeds OFF;"
        );
    }
}
