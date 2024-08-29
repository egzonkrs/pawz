using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Mappings;
using Pawz.Domain.Abstractions;
using Pawz.Web.Mapping;
using System;

namespace Pawz.Web.Modules;

public class CoreModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(typeof(MappingProfiles).Assembly, typeof(WebMappProfile).Assembly);
    }
}
