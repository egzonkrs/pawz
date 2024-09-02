using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Mappings;
using Pawz.Domain.Abstractions;
using Pawz.Web.Mapping;

namespace Pawz.Web.Modules;

public class CoreModule : IModule
{
        public void Load(IServiceCollection services)
        {
                services.AddControllersWithViews();
                services.AddAutoMapper(typeof(ApplicationMappingProfiles).Assembly, typeof(WebMappingProfile).Assembly);
        }
}
