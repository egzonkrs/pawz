using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Interfaces;
using Pawz.Application.Services;
using Pawz.Domain.Abstractions;
using Pawz.Domain.Entities;
using Pawz.Infrastructure.Data;

namespace Pawz.Web.Modules;

public class AuthModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();
    }
}
