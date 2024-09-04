using Microsoft.EntityFrameworkCore;
using Pawz.Application.Helpers;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPetImages
{
    public static async Task SeedPetImageData(AppDbContext context)
    {
        var petImagesExist = await context.PetImages.AnyAsync();
        if (petImagesExist) return;

        var uploadedAt = DateTime.UtcNow;

        //Dog Images
        var firstImageUrlForPet1 = await PetImageFetcher.FetchRandomDogImageUrlAsync();
        var secondImageUrlForPet1 = await PetImageFetcher.FetchRandomDogImageUrlAsync();
        var thirdImageUrlForPet1 = await PetImageFetcher.FetchRandomDogImageUrlAsync();
        var fourthImageUrlForPet1 = await PetImageFetcher.FetchRandomDogImageUrlAsync();

        var firstImageUrlForPet2 = await PetImageFetcher.FetchRandomDogImageUrlAsync();
        var secondImageUrlForPet2 = await PetImageFetcher.FetchRandomDogImageUrlAsync();
        var thirdImageUrlForPet2 = await PetImageFetcher.FetchRandomDogImageUrlAsync();
        var fourthImageUrlForPet2 = await PetImageFetcher.FetchRandomDogImageUrlAsync();

        //Cat Images
        var firstImageUrlForPet3 = await PetImageFetcher.FetchRandomCatImageUrlAsync();
        var secondImageUrlForPet3 = await PetImageFetcher.FetchRandomCatImageUrlAsync();
        var thirdImageUrlForPet3 = await PetImageFetcher.FetchRandomCatImageUrlAsync();
        var fourthImageUrlForPet3 = await PetImageFetcher.FetchRandomCatImageUrlAsync();

        var firstImageUrlForPet4 = await PetImageFetcher.FetchRandomCatImageUrlAsync();
        var secondImageUrlForPet4 = await PetImageFetcher.FetchRandomCatImageUrlAsync();
        var thirdImageUrlForPet4 = await PetImageFetcher.FetchRandomCatImageUrlAsync();
        var fourthImageUrlForPet4 = await PetImageFetcher.FetchRandomCatImageUrlAsync();

        var petImages = new List<PetImage>
        {
                new PetImage
                {
                    PetId = 1,
                    ImageUrl = firstImageUrlForPet1,
                    IsPrimary = true,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 1,
                    ImageUrl = secondImageUrlForPet1,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 1,
                    ImageUrl = thirdImageUrlForPet1,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 1,
                    ImageUrl = fourthImageUrlForPet1,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 2,
                    ImageUrl = firstImageUrlForPet2,
                    IsPrimary = true,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 2,
                    ImageUrl = secondImageUrlForPet2,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 2,
                    ImageUrl = thirdImageUrlForPet2,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 2,
                    ImageUrl = fourthImageUrlForPet2,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 3,
                    ImageUrl = firstImageUrlForPet3,
                    IsPrimary = true,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },

                new PetImage
                {
                    PetId = 3,
                    ImageUrl = secondImageUrlForPet3,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },

                new PetImage
                {
                    PetId = 3,
                    ImageUrl = thirdImageUrlForPet3,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },

                new PetImage
                {
                    PetId = 3,
                    ImageUrl = fourthImageUrlForPet3,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 4,
                    ImageUrl = firstImageUrlForPet4,
                    IsPrimary = true,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },

                new PetImage
                {
                    PetId = 4,
                    ImageUrl = secondImageUrlForPet4,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 4,
                    ImageUrl = thirdImageUrlForPet4,
                    IsPrimary = false,
                    UploadedAt = uploadedAt,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new PetImage
                {
                    PetId = 4,
                    ImageUrl = fourthImageUrlForPet4,
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
