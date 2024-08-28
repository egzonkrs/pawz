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
                Name = "Pristina",
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
                Name = "Tirana",
                CountryId = 2,
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Cities.AddRange(cities);
        await context.SaveChangesAsync();
    }
}
