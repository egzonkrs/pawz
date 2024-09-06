using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedAdoptionRequests
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
                Status = AdoptionRequestStatus.Pending,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 1,
                LocationId = 3,
                RequesterUserId = userBob.Id,
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Pending,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 1,
                LocationId = 3,
                RequesterUserId = userJane.Id,
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
                RequesterUserId = userBob.Id,
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
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Approved,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                PetId = 3,
                LocationId = 3,
                RequesterUserId = userJohn.Id,
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status = AdoptionRequestStatus.Approved,
                RequestDate = DateTime.UtcNow,
                ResponseDate = DateTime.UtcNow,
                LocationId = 3,
                PetId = 3,
                RequesterUserId = userBob.Id,
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.AdoptionRequests.AddRange(adoptionRequests);
        await context.SaveChangesAsync();
    }
}
