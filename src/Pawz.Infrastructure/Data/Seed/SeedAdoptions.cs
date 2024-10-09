using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public static class SeedAdoptions
{
    public static async Task SeedAdoptionData(AppDbContext context)
    {
        var adoptionsExists = await context.Adoptions.AnyAsync();
        if (adoptionsExists) return;

        var adoptions = new List<Adoption>
        {
            new Adoption
            {
                AdoptionRequestId = 3,
                AdoptionDate = DateTime.UtcNow,
                AdoptionFee = 0,
                IsDeleted = false,
                DeletedAt = null
            },
            new Adoption
            {
                AdoptionRequestId = 4,
                AdoptionDate = DateTime.UtcNow,
                AdoptionFee = 0,
                IsDeleted = false,
                DeletedAt = null
            },
        };

        context.Adoptions.AddRange(adoptions);
        await context.SaveChangesAsync();
    }
}
