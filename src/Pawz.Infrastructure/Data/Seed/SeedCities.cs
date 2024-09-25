using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;
public class SeedCities
{
    public static async Task SeedCityData(AppDbContext context)
    {
        var citiesExist = await context.Cities.AnyAsync();
        if (citiesExist) return;

        var cities = new List<City>
        {
            new City
            {
                Name = "Prishtinë",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Prizren",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Mitrovicë",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Pejë",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Ferizaj",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Vushtrri",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Tiranë",
                CountryId = 2,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Durrës",
                CountryId = 2,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Shkodër",
                CountryId = 2,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Vlorë",
                CountryId = 2,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Elbasan",
                CountryId = 2,
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Cities.AddRange(cities);
        await context.SaveChangesAsync();
    }
}
