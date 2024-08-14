using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Interfaces;

namespace Pawz.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddModule<T>(this IServiceCollection services) where T : IModule, new()
    {
        var module = new T();
        module.Load(services);
    }
}
