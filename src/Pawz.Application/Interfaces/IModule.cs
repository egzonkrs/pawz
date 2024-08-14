using Microsoft.Extensions.DependencyInjection;

namespace Pawz.Application.Interfaces;
public interface IModule
{
    /// <summary>
    /// Loads the Dependency Injection Registrations.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    public void Load(IServiceCollection services);
}
