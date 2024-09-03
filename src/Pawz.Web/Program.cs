using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using Pawz.Infrastructure.Data.Seed;
using Pawz.Web.Extensions;
using Pawz.Web.Modules;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModule(new CoreModule());
builder.Services.AddModule(new AuthModule());
builder.Services.AddModule(new ValidationModule());
builder.Services.AddModule(new DataModule(builder.Configuration));

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

await app.UseDataSeeder();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();