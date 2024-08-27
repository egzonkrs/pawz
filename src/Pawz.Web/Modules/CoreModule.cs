using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Interfaces;
using Pawz.Domain.Abstractions;
using Pawz.Infrastructure.Services;
using System;

namespace Pawz.Web.Modules;

public class CoreModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IFileUploaderService, FileUploaderService>();

    }
}
