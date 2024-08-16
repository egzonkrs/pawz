using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pawz.Domain.Abstractions;
using Pawz.Web.Models;
using System;

namespace Pawz.Web.Modules;

public class CoreModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
