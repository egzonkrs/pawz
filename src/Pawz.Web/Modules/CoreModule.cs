using Asp.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Mappings;
using Pawz.Domain.Abstractions;
using Pawz.Web.Configurations;
using Pawz.Web.Mapping;

namespace Pawz.Web.Modules;

public class CoreModule : IModule
{
    private readonly IConfiguration _configuration;

    public CoreModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Load(IServiceCollection services)
    {
        services.Configure<ApiSettings>(_configuration.GetSection(ApiSettings.SectionName));
        services.AddControllersWithViews();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddAutoMapper(typeof(ApplicationMappingProfiles).Assembly, typeof(WebMappingProfile).Assembly);
        services.AddSignalR();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
    }
}
