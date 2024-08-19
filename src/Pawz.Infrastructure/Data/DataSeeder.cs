using Microsoft.AspNetCore.Identity;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
    public class DataSeeder
    {
        public static async Task SeedData(AppDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            await SeedSpecies.SeedSpeciesData(context);

            await SeedBreeds.SeedBreedData(context);

            await SeedLocations.SeedLocationData(context);

            await SeedUsers.SeedUserData(userManager);

            await SeedPets.SeedPetData(context);

            await SeedPetImages.SeedPetImageData(context);

            await unitOfWork.SaveChangesAsync();
        }
    }
}