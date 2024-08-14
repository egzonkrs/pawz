using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Interfaces;
using Pawz.Infrastructure.Services;

namespace Pawz.Application.Services;

/// <summary>
/// Configures and registers application layer services with the dependency injection container.
/// </summary>
public class ModuleService : IModule
{
    /// <summary>
    /// Registers application layer services with the provided service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public void Load(IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IAdoptionService, AdoptionService>();
    }
}
