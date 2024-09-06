using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedSpecies
{
    public static async Task SeedSpeciesData(AppDbContext context)
    {
        var speciesExist = await context.Species.AnyAsync();
        if (speciesExist) return;

        var species = new List<Species>
        {
            new()
            {
                Name = "Dog",
                Description = "Domesticated carnivorous mammal",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new()
            {
                Name = "Cat",
                Description = "Small domesticated carnivorous mammal",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        context.Species.AddRange(species);
        await context.SaveChangesAsync();
    }
}
