using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPetImages
{
    public static async Task SeedPetImageData(AppDbContext context)
    {
        var petImagesExist = await context.PetImages.AnyAsync();
        if (petImagesExist) return;

        var uploadedAt = DateTime.UtcNow;

        var petImages = new List<PetImage>
        {
            new PetImage
            {
                PetId = 1,
                ImageUrl = "https://picsum.photos/id/237/200/300",
                IsPrimary = true,
                UploadedAt = uploadedAt,
                IsDeleted = false,
                DeletedAt = null
            },
            new PetImage
            {
                PetId = 2,
                ImageUrl = "https://picsum.photos/id/238/200/300",
                IsPrimary = false,
                UploadedAt = uploadedAt,
                IsDeleted = false,
                DeletedAt = null
            },
            new PetImage
            {
                PetId = 3,
                ImageUrl = "https://picsum.photos/id/239/200/300",
                IsPrimary = true,
                UploadedAt = uploadedAt,
                IsDeleted = false,
                DeletedAt = null
            },
            new PetImage
            {
                PetId = 4,
                ImageUrl = "https://picsum.photos/id/240/200/300",
                IsPrimary = false,
                UploadedAt = uploadedAt,
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.PetImages.AddRange(petImages);
        await context.SaveChangesAsync();
    }
}
