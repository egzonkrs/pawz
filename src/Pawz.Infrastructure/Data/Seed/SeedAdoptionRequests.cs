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

        var userJohn = await context.Users.FirstOrDefaultAsync(u => u.UserName == "john");
        var userJane = await context.Users.FirstOrDefaultAsync(u => u.UserName == "jane");

        var adoptionRequests = new List<AdoptionRequest>
        {
            new AdoptionRequest
            {
                Status=AdoptionRequestStatus.Pending,
                RequestDate=DateTime.UtcNow,
                ResponseDate=DateTime.UtcNow,
                PetId=1,
                RequesterUserId=userJane.Id,
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status=AdoptionRequestStatus.Pending,
                RequestDate=DateTime.UtcNow,
                ResponseDate=DateTime.UtcNow,
                PetId=2,
                RequesterUserId=userJohn.Id,
                IsDeleted = false,
                DeletedAt = null
            },
            new AdoptionRequest
            {
                Status=AdoptionRequestStatus.Rejected,
                RequestDate=DateTime.UtcNow,
                ResponseDate=DateTime.UtcNow,
                PetId=3,
                RequesterUserId=userJane.Id,
                IsDeleted = false,
                DeletedAt = null
            },
        };

        context.AdoptionRequests.AddRange(adoptionRequests);
        await context.SaveChangesAsync();
    }
}
