using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPetImages
{
    public static async Task SeedPetImageData(AppDbContext context)
    {
        var petImagesExists = await context.PetImages.AnyAsync();
        if (petImagesExists) return;

        var petImages = new List<PetImage>
        {
            // PetId 1 - Buddy (Golden Retriever)
            new PetImage
            {
                PetId = 1,
                ImageUrl = "dog1_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 1,
                ImageUrl = "dog1_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 1,
                ImageUrl = "dog1_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 1,
                ImageUrl = "dog1_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 2 - Max (Labrador Retriever)
            new PetImage
            {
                PetId = 2,
                ImageUrl = "dog2_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 2,
                ImageUrl = "dog2_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 2,
                ImageUrl = "dog2_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 3 - Whiskers (Persian Cat)
            new PetImage
            {
                PetId = 3,
                ImageUrl = "cat1_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 3,
                ImageUrl = "cat1_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 3,
                ImageUrl = "cat1_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 3,
                ImageUrl = "cat1_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 2 - Max (Labrador Retriever)
            new PetImage
            {
                PetId = 4,
                ImageUrl = "dog3_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 4,
                ImageUrl = "dog3_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 4,
                ImageUrl = "dog3_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 4,
                ImageUrl = "dog3_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 5 - Mittens (Siamese Cat)
            new PetImage
            {
                PetId = 5,
                ImageUrl = "cat2_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 5,
                ImageUrl = "cat2_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 5,
                ImageUrl = "cat2_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 5,
                ImageUrl = "cat2_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 6 - Shadow (Maine Coon)
            new PetImage
            {
                PetId = 6,
                ImageUrl = "cat3_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 6,
                ImageUrl = "cat3_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 6,
                ImageUrl = "cat3_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 6,
                ImageUrl = "cat3_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            }
        };

        context.PetImages.AddRange(petImages);
        await context.SaveChangesAsync();
    }
}
