using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public static class SeedAdoptionRequests
{
    public static async Task SeedAdoptionRequestData(AppDbContext context)
    {
        var adoptionRequestsExists = await context.AdoptionRequests.AnyAsync();
        if (adoptionRequestsExists) return;

        var userAsd = await context.Users.FirstOrDefaultAsync(u => u.Email == "asd@qwe.com");
        var userBob = await context.Users.FirstOrDefaultAsync(u => u.Email == "bob@example.com");
        var userJane = await context.Users.FirstOrDefaultAsync(u => u.Email == "jane@example.com");
        var userJohn = await context.Users.FirstOrDefaultAsync(u => u.Email == "john@example.com");

        var adoptionRequests = new List<AdoptionRequest>
        {
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Approved,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 1,
                LocationId = 3,
                RequesterUserId = userBob.Id,
                ContactNumber = "+383 44 111 222",
                Email = "bob@example.com",
                IsRentedProperty = false,
                HasOutdoorSpace = true,
                OutdoorSpaceDetails = "Large backyard",
                OwnsOtherPets = false,
                OtherPetsDetails = null,
                Message = "Looking forward to adopting.",
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Rejected,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 1,
                LocationId = 3,
                RequesterUserId = userJane.Id,
                ContactNumber = "+383 44 222 333",
                Email = "jane@example.com",
                IsRentedProperty = true,
                HasOutdoorSpace = false,
                OutdoorSpaceDetails = null,
                OwnsOtherPets = true,
                OtherPetsDetails = "2 cats, 1 dog",
                Message = "Ready to welcome a new pet.",
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Rejected,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 2,
                LocationId = 3,
                RequesterUserId = userBob.Id,
                ContactNumber = "+383 44 111 222",
                Email = "bob@example.com",
                IsRentedProperty = false,
                HasOutdoorSpace = true,
                OutdoorSpaceDetails = "Small balcony",
                OwnsOtherPets = false,
                OtherPetsDetails = null,
                Message = "Excited for this pet.",
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Approved,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 2,
                LocationId = 3,
                RequesterUserId = userJane.Id,
                ContactNumber = "+383 44 222 333",
                Email = "jane@example.com",
                IsRentedProperty = true,
                HasOutdoorSpace = false,
                OutdoorSpaceDetails = null,
                OwnsOtherPets = true,
                OtherPetsDetails = "1 rabbit",
                Message = "Can't wait to adopt!",
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Pending,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 3,
                LocationId = 3,
                RequesterUserId = userJohn.Id,
                ContactNumber = "+383 44 333 444",
                Email = "john@example.com",
                IsRentedProperty = false,
                HasOutdoorSpace = true,
                OutdoorSpaceDetails = "Huge garden",
                OwnsOtherPets = false,
                OtherPetsDetails = null,
                Message = "This is the perfect pet for me.",
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Pending,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                LocationId = 3,
                PetId = 3,
                RequesterUserId = userBob.Id,
                ContactNumber = "+383 44 111 222",
                Email = "bob@example.com",
                IsRentedProperty = false,
                HasOutdoorSpace = true,
                OutdoorSpaceDetails = "Patio area",
                OwnsOtherPets = false,
                OtherPetsDetails = null,
                Message = "Happy to welcome a new pet.",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.AdoptionRequests.AddRange(adoptionRequests);
        await context.SaveChangesAsync();
    }
}
