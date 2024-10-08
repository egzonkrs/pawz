using Microsoft.AspNetCore.Identity;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data.Seed;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedData(AppDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        await SeedUsers.SeedUserData(userManager);
        await SeedSpecies.SeedSpeciesData(context);
        await SeedBreeds.SeedBreedData(context);
        await SeedCountries.SeedCountryData(context);
        await SeedCities.SeedCityData(context);
        await SeedLocations.SeedLocationData(context);
        await SeedPets.SeedPetData(context);
        await SeedPetImages.SeedPetImageData(context);
        await SeedAdoptionRequests.SeedAdoptionRequestData(context);
        await SeedAdoptions.SeedAdoptionData(context);

        await unitOfWork.SaveChangesAsync();
    }
}
