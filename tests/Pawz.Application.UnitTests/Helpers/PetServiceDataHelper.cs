using Bogus;
using Microsoft.AspNetCore.Http;
using Pawz.Application.Models;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawz.Application.UnitTests.Helpers;

public static class PetServiceDataHelper
{
    public static PetCreateRequest GenerateValidPetCreateRequest()
    {
        var faker = new Faker();

        return new PetCreateRequest
        {
            Name = faker.Name.FirstName(),
            BreedId = faker.Random.Int(1, 10),
            AgeYears = faker.Random.Int(1, 10).ToString() + " months",
            About = faker.Lorem.Sentence(),
            Price = faker.Random.Decimal(50, 500),
            CityId = faker.Random.Int(1, 100),
            Address = faker.Address.StreetAddress(),
            PostalCode = faker.Address.ZipCode(),
            ImageFiles = new List<IFormFile>(),
            Status = PetStatus.Available
        };
    }

    public static Location GenerateValidLocation()
    {
        var faker = new Faker();

        return new Location
        {
            CityId = faker.Random.Int(1, 100),
            Address = faker.Address.StreetAddress(),
            PostalCode = faker.Address.ZipCode()
        };
    }

    public static Pet GenerateValidPet()
    {
        var faker = new Faker();

        return new Pet
        {
            Id = faker.Random.Int(1, 1000),
            Name = faker.Name.FirstName(),
            BreedId = faker.Random.Int(1, 10),
            AgeYears = faker.Random.Int(1, 10).ToString() + " months",
            About = faker.Lorem.Sentence(),
            Price = faker.Random.Decimal(50, 500),
            Location = GenerateValidLocation(),
            PostedByUserId = faker.Name.FullName(),
            Status = PetStatus.Available
        };
    }
}
