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
            },

            // PetId 7 - Llamba (German Shepard)
            new PetImage
            {
                PetId = 7,
                ImageUrl = "dog4_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 7,
                ImageUrl = "dog4_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 7,
                ImageUrl = "dog4_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 7,
                ImageUrl = "dog4_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 8 - Ollie (Labrador Retriever)
            new PetImage
            {
                PetId = 8,
                ImageUrl = "dog5_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 8,
                ImageUrl = "dog5_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 8,
                ImageUrl = "dog5_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 8,
                ImageUrl = "dog5_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 9 - Milo (Persian)
            new PetImage
            {
                PetId = 9,
                ImageUrl = "cat4_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 9,
                ImageUrl = "cat4_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 9,
                ImageUrl = "cat4_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 9,
                ImageUrl = "cat4_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 10 - Simba (Persian)
            new PetImage
            {
                PetId = 10,
                ImageUrl = "cat5_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 10,
                ImageUrl = "cat5_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 10,
                ImageUrl = "cat5_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 10,
                ImageUrl = "cat5_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 11 - Simona (Siamese)
            new PetImage
            {
                PetId = 11,
                ImageUrl = "cat6_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 11,
                ImageUrl = "cat6_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 11,
                ImageUrl = "cat6_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 11,
                ImageUrl = "cat6_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 12 - Donny (Labrador Retriever)
            new PetImage
            {
                PetId = 12,
                ImageUrl = "dog6_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 12,
                ImageUrl = "dog6_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 12,
                ImageUrl = "dog6_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 12,
                ImageUrl = "dog6_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 13 - Nala (German Shepard)
            new PetImage
            {
                PetId = 13,
                ImageUrl = "dog7_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 13,
                ImageUrl = "dog7_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 13,
                ImageUrl = "dog7_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 13,
                ImageUrl = "dog7_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 14 - Dylan (Golden Retriver)
            new PetImage
            {
                PetId = 14,
                ImageUrl = "dog8_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 14,
                ImageUrl = "dog8_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 14,
                ImageUrl = "dog8_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 14,
                ImageUrl = "dog8_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 15 - Mia (Siamese)
            new PetImage
            {
                PetId = 15,
                ImageUrl = "cat7_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 15,
                ImageUrl = "cat7_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 15,
                ImageUrl = "cat7_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 15,
                ImageUrl = "cat7_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            },

            // PetId 16 - Bubbles (Maine Coon)
            new PetImage
            {
                PetId = 16,
                ImageUrl = "cat8_1.jpg",
                IsPrimary = true,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 16,
                ImageUrl = "cat8_2.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 16,
                ImageUrl = "cat8_3.jpg",
                IsPrimary = false,
                IsDeleted = false
            },
            new PetImage
            {
                PetId = 16,
                ImageUrl = "cat8_4.jpg",
                IsPrimary = false,
                IsDeleted = false
            }
        };

        context.PetImages.AddRange(petImages);
        await context.SaveChangesAsync();
    }
}
