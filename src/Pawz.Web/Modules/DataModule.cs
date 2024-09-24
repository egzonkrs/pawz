using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Helpers;
using Pawz.Application.Interfaces;
using Pawz.Application.Services;
using Pawz.Domain.Abstractions;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using Pawz.Infrastructure.Repos;
using Pawz.Infrastructure.Services;
using Pawz.Web.Hubs;
using System.Configuration;

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
                .UseSqlServer(connectionString)
                .AddInterceptors(new SoftDeleteInterceptor())
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
        services.AddScoped<IBreedRepository, BreedRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddScoped<IPetImageRepository, PetImageRepository>();
        services.AddScoped<IFileUploaderService, FileUploaderService>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        services.AddScoped<IPetService, PetService>();
        services.AddScoped<ISpeciesService, SpeciesService>();
        services.AddScoped<IBreedService, BreedService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IAdoptionRequestService, AdoptionRequestService>();
        services.AddScoped<INotificationService, NotificationService>();

        services.Configure<ApiSettings>(_configuration.GetSection(ApiSettings.SectionName));

        services.AddScoped<IRealTimeNotificationSender, SignalRNotificationSender>();
        services.AddScoped<INotificationHubContext, SignalRHubContext>();
        services.AddScoped<IRealTimeNotificationSender, RealTimeNotificationSender>();
    }
}
