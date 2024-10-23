using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace Pawz.Web.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task UseDataSeeder(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting database migration and data seeding.");

            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var redisRepository = services.GetRequiredService<IRedisRepository>();

            await context.Database.MigrateAsync();
            logger.LogInformation("Database migration completed.");

            await DataSeeder.SeedData(context, unitOfWork, userManager, redisRepository);
            logger.LogInformation("Data seeding completed.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}
