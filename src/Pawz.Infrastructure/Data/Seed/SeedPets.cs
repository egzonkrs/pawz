using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPets
{
    public static async Task SeedPetData(AppDbContext context)
    {
        var petsExists = await context.Pets.AnyAsync();
        if (petsExists) return;

        var userJohn = await context.Users.FirstOrDefaultAsync(u => u.UserName == "john");
        var userJane = await context.Users.FirstOrDefaultAsync(u => u.UserName == "jane");

        var pets = new List<Pet>
        {
            new Pet
            {
                Name = "Buddy",
                BreedId = 1,
                AgeYears = "3-7 Years",
                About = "Friendly and playful dog.",
                Price = 300.00m,
                Status = PetStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1,
                PostedByUserId = userJohn.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Max",
                BreedId = 2,
                AgeYears = "0-3 Months",
                About = "Loyal and intelligent dog.",
                Price = 250.00m,
                Status = PetStatus.Approved,
                CreatedAt = DateTime.UtcNow,
                LocationId = 2,
                PostedByUserId = userJane.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Whiskers",
                BreedId = 3,
                AgeYears = "3-7 Years",
                About = "Quiet and affectionate cat.",
                Price = 150.00m,
                Status = PetStatus.Rejected,
                CreatedAt = DateTime.UtcNow,
                LocationId = 3,
                PostedByUserId = userJohn.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Mittens",
                BreedId = 4,
                AgeYears = "3-6 Months",
                About = "Active and social cat.",
                Price = 200.00m,
                Status = PetStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1,
                PostedByUserId = userJane.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Rocky",
                BreedId = 4,
                AgeYears = 2,
                AgeMonths = 7,
                About = "Active and social dog.",
                Price = 200.00m,
                Status = PetStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1,
                PostedByUserId = userJane.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Kage",
                BreedId = 4,
                AgeYears = 3,
                AgeMonths = 3,
                About = "Active and social dog.",
                Price = 200.00m,
                Status = PetStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1,
                PostedByUserId = userJane.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Pobo",
                BreedId = 4,
                AgeYears = 3,
                AgeMonths = 3,
                About = "Active and social dog.",
                Price = 200.00m,
                Status = PetStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1,
                PostedByUserId = userJane.Id,
                IsDeleted = false
            }
        };

        context.Pets.AddRange(pets);
        await context.SaveChangesAsync();
    }
}
