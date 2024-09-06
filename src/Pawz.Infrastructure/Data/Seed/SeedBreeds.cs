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
            new()
            {
                SpeciesId = 1, // Dog
                Name = "Golden Retriever",
                Description = "Friendly, intelligent, and devoted breed, known for its golden coat.",
                IsDeleted = false,
                DeletedAt = null
            },
            new()
            {
                SpeciesId = 1, // Dog
                Name = "Labrador Retriever",
                Description = "Outgoing, even-tempered, and playful breed, one of the most popular family dogs.",
                IsDeleted = false,
                DeletedAt = null
            },
            new()
            {
                SpeciesId = 1, // Dog
                Name = "German Shepherd",
                Description = "Courageous, confident, and smart breed, often used in working roles like police and military.",
                IsDeleted = false,
                DeletedAt = null
            },

            // Cat Breeds
            new()
            {
                SpeciesId = 2, // Cat
                Name = "Persian",
                Description = "Affectionate and quiet breed with long, luxurious fur and a calm demeanor.",
                IsDeleted = false,
                DeletedAt = null
            },
            new()
            {
                SpeciesId = 2, // Cat
                Name = "Siamese",
                Description = "Social, intelligent, and vocal breed with striking blue eyes and sleek body.",
                IsDeleted = false,
                DeletedAt = null
            },
            new()
            {
                SpeciesId = 2, // Cat
                Name = "Maine Coon",
                Description = "Large, friendly breed with a bushy tail and long fur, known for its gentle personality.",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Breeds.AddRange(breeds);
        await context.SaveChangesAsync();
    }
}
