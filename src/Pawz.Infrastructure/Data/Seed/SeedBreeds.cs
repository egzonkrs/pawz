using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedBreeds
{
    public static async Task SeedBreedData(AppDbContext context)
    {
        var breedsExists = await context.Breeds.AnyAsync();
        if (breedsExists) return;

        var breeds = new List<Breed>
        {
            new Breed
            {
                SpeciesId = 1,
                Name = "Golden Retriever",
                Description = "Friendly and tolerant breed.",
                IsDeleted = false,
                DeletedAt = null
            },
            new Breed
            {
                SpeciesId = 1,
                Name = "Labrador Retriever",
                Description = "Outgoing and even-tempered breed.",
                IsDeleted = false,
                DeletedAt = null
            },
            new Breed
            {
                SpeciesId = 2,
                Name = "Persian",
                Description = "Affectionate and quiet breed with long fur.",
                IsDeleted = false,
                DeletedAt = null
            },
            new Breed
            {
                SpeciesId = 2,
                Name = "Siamese",
                Description = "Social and intelligent breed with striking blue eyes.",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Breeds.AddRange(breeds);
        await context.SaveChangesAsync();
    }
}
