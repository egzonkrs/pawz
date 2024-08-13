using Microsoft.AspNetCore.Hosting;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Pawz.Web.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        // Register FluentValidation services
        services.AddValidatorsFromAssemblyContaining<Program>(); // Adjust the assembly to match where your validators are defined
        return services;
    }
}
