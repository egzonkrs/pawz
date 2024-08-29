using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedAdoptions
{
    public static async Task SeedAdoptionData(AppDbContext context)
    {
        var adoptionsExists = await context.Adoptions.AnyAsync();
        if (adoptionsExists) return;


        var adoptions = new List<Adoption>
        {
            new Adoption
            {
                AdoptionRequestId=1,
                AdoptionDate=DateTime.UtcNow,
                AdoptionFee=1,
                IsDeleted = false,
                DeletedAt = null
            },
            new Adoption
            {
                AdoptionRequestId=2,
                AdoptionDate=DateTime.UtcNow,
                AdoptionFee=2,
                IsDeleted = false,
                DeletedAt = null
            },
            new Adoption
            {
                AdoptionRequestId=3,
                AdoptionDate=DateTime.UtcNow,
                AdoptionFee=3,
                IsDeleted = false,
                DeletedAt = null
            },
        };

        context.Adoptions.AddRange(adoptions);
        await context.SaveChangesAsync();
    }
}
