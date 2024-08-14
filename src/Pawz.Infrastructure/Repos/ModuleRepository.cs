using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Interfaces;
using Pawz.Domain.Interfaces;

namespace Pawz.Infrastructure.Repos;
public class ModuleRepository : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAdoptionRepository, AdoptionRepository>();
        services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
        services.AddScoped<IPetRepository, PetRepository>();

    }
}
