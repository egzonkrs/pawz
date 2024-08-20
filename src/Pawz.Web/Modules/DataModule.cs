using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pawz.Domain.Abstractions;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using Pawz.Infrastructure.Data.Seed;
using Pawz.Infrastructure.Repos;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Pawz.Web.Modules;

public class DataModule : IModule
{
    private readonly IConfiguration _configuration;

    public DataModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Load(IServiceCollection services)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        if (connectionString is null)
        {
            throw new ConfigurationErrorsException("Cannot find 'DefaultConnection' section inside the configuration");
        }

        services.AddDbContext<AppDbContext>(options =>
            options
                .UseSqlite(connectionString)
                .AddInterceptors(new SoftDeleteInterceptor())
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
        services.AddScoped<IBreedRepository, BreedRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
    }

    public static async Task ApplyMigrationsAndSeedDataAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            await context.Database.MigrateAsync();
            await DataSeeder.SeedData(context, unitOfWork, userManager);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}
