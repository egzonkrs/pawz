using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public static class SeedPets
{
    public static async Task SeedPetData(AppDbContext context)
    {
        var petsExists = await context.Pets.AnyAsync();
        if (petsExists) return;

        var userAsd = await context.Users.FirstOrDefaultAsync(u => u.Email == "asd@qwe.com");
        var userBob = await context.Users.FirstOrDefaultAsync(u => u.Email == "bob@example.com");

        var pets = new List<Pet>
        {
            // Pets assigned to userAsd (2 dogs, 1 cat)
            new Pet
            {
                Name = "Buddy",
                BreedId = 1, // Golden Retriever
                AgeYears = "3-7 Years",
                About = "Buddy is the perfect adventure buddy! Whether it's a long walk in the park or just chilling at home, he’s always there to make you smile with his playful and loyal personality. Loves belly rubs and fetch!",
                Price = 0.00m, // Free
                Status = PetStatus.Approved,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1, // Pristina
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Max",
                BreedId = 2, // Labrador Retriever
                AgeYears = "0-3 Months",
                About = "Max is a little bundle of joy who’s still learning his way around the world! He’s full of energy and loves meeting new people. If you’re looking for a loyal and goofy companion, Max is your guy!",
                Price = 0.00m, // Free
                Status = PetStatus.Approved,
                CreatedAt = DateTime.UtcNow,
                LocationId = 6, // Tirana
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Whiskers",
                BreedId = 4, // Persian
                AgeYears = "3-7 Years",
                About = "Whiskers is the ultimate lap cat! With a fluffy coat, he's the perfect companion for those quiet, cozy nights. He enjoys soft purring naps and watching the world go by from the window.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 3, // Mitrovica
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },

            // Pets assigned to userBob (1 dog, 2 cats)
            new Pet
            {
                Name = "Rocky",
                BreedId = 2, // Beagle
                AgeYears = "3-6 Months",
                About = "Rocky is an energetic, curious little pup who loves to explore everything! Whether he's sniffing out new trails or playing in the yard, he’s always on the move. Perfect for active families who love the outdoors!",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 6, // Shkoder
                PostedByUserId = userBob.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Mittens",
                BreedId = 5, // Siamese
                AgeYears = "3-6 Months",
                About = "Mittens is a talkative and social little furball who loves to chat with her humans! She’s always curious and loves to be part of the action. With her striking blue eyes, she’ll steal your heart in no time!",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 4, // Peja
                PostedByUserId = userBob.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Shadow",
                BreedId = 6, // Maine Coon
                AgeYears = "3-7 Years",
                About = "Shadow is a gentle giant with a heart full of love! Despite his size, he's a calm and affectionate cat who enjoys cuddling up by your side. He’s independent but always down for some quality petting time.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 6, // Tirana
                PostedByUserId = userBob.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Llamba",
                BreedId = 3, // German Shepherd
                AgeYears = "1-2 Years",
                About = "Llaba is a mixture of staffy and border collie, body, his coat is short, but soft like a border Collie. His torso coat is predominantly brown with darker liver merle patterns like an Australian Shepherd.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 2, // Prizren
                PostedByUserId = userBob.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Ollie",
                BreedId = 2, // Labrador Retriever
                AgeYears = "2-4 Years",
                About = "Ollie is so good with other dogs and knows how to appropriately correct boisterous dogs without causing injury and has been very useful in training sessions and has positive experiences during social time.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 3, // Mitrovica
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Milo",
                BreedId = 4, // Persian
                AgeYears = "1-2 Years",
                About = "Ollie is so good with other dogs and knows how to appropriately correct boisterous dogs without causing injury and has been very useful in training sessions and has positive experiences during social time.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 3, // Prizren
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Simba",
                BreedId = 4, // Persian
                AgeYears = "3-4 Years",
                About = "Luna is an endearing boy whose charming personality and delightful quirks make him an irresistible companion. If you're looking for a friend who exudes warmth and love, Robert could be your purr-fect match.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 6, // Tirana
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Simona",
                BreedId = 5, // Siamese
                AgeYears = "2-3 Years",
                About = "Siamese is an endearing boy whose charming personality and delightful quirks make him an irresistible companion. If you're looking for a friend who exudes warmth and love, Robert could be your purr-fect match.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 6, // Tirana
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Donny",
                BreedId = 2, // Labrador Retriever
                AgeYears = "2-4 Years",
                About = "Donny is so good with other dogs and knows how to appropriately correct boisterous dogs without causing injury and has been very useful in training sessions and has positive experiences during social time.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 3, // Mitrovica
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Nala",
                BreedId = 3, // German Shepard
                AgeYears = "1-2 Years",
                About = "Nala is a sweet and gentle girl who is blossoming into a confident and loving companion. She has a tender heart and an eagerness to learn, making her an ideal addition to a family that can provide her with the patience.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 3, // Mitrovica
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Dylan",
                BreedId = 1, // Golden Retriver
                AgeYears = "2-4 Years",
                About = "Dylan is an energetic and affectionate young boy looking for a loving home. He loves playing games, loving companion and would be suited to an active family with older kids who will include him on all their adventures.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 3, // Mitrovica
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Mia",
                BreedId = 5, // Siamese
                AgeYears = "2-3 Years",
                About = "Mia is an endearing boy whose charming personality and delightful quirks make him an irresistible companion. If you're looking for a friend who exudes warmth and love, Robert could be your purr-fect match.",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 6, // Tirana
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Bubbles",
                BreedId = 6, // Maine Coon
                AgeYears = "2-3 Years",
                About = "Bubbles exhibits an adventurous spirit, but also doesn't mind a good catnap with their human. This cute girl thrives on playtimes and an exciting fancy soiree, tuna and chicken being her favourite tea time treats..",
                Price = 0.00m, // Free
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 6, // Tirana
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            }
        };

        context.Pets.AddRange(pets);
        await context.SaveChangesAsync();
    }
}
