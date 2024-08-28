using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;
public class SeedCountries
{
    public static async Task SeedCountryData(AppDbContext context)
    {
        var countriesExist = await context.Countries.AnyAsync();
        if (countriesExist) return;

        var countries = new List<Country>
        {
            new Country
            {
                Name = "Kosova",
                IsDeleted = false,
                DeletedAt = null
            },
            new Country
            {
                Name = "Albania",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Countries.AddRange(countries);
        await context.SaveChangesAsync();
    }
}
