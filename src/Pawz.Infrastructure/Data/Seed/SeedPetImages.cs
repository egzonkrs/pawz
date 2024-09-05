using Microsoft.EntityFrameworkCore;
using Pawz.Application.Helpers;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPetImages
{
    public static async Task SeedPetImageData(AppDbContext context)
    {
        if (await context.PetImages.AnyAsync()) return;

        var petImages = new List<PetImage>();

        // Seed Dog Images
        petImages.AddRange(await CreatePetImagesAsync(petId: 1, imageCount: 4, isDog: true));
        petImages.AddRange(await CreatePetImagesAsync(petId: 2, imageCount: 4, isDog: true));

        // Seed Cat Images
        petImages.AddRange(await CreatePetImagesAsync(petId: 3, imageCount: 4, isDog: false));
        petImages.AddRange(await CreatePetImagesAsync(petId: 4, imageCount: 4, isDog: false));

        context.PetImages.AddRange(petImages);
        await context.SaveChangesAsync();
    }

    private static async Task<IEnumerable<PetImage>> CreatePetImagesAsync(int petId, int imageCount, bool isDog)
    {
        var imageTasks = new List<Task<string>>();

        for (int i = 0; i < imageCount; i++)
        {
            var fetchTask = isDog
                ? PetImageFetcher.FetchRandomDogImageUrlAsync()
                : PetImageFetcher.FetchRandomCatImageUrlAsync();
            imageTasks.Add(fetchTask);
        }

        var imageUrls = await Task.WhenAll(imageTasks);

        var images = new List<PetImage>();
        for (int i = 0; i < imageUrls.Length; i++)
        {
            images.Add(new PetImage
            {
                PetId = petId,
                ImageUrl = imageUrls[i],
                IsPrimary = i == 0,
                IsDeleted = false,
                DeletedAt = null
            });
        }
        return images;
    }
}
