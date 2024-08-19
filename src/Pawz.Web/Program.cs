using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Pawz.Web.Extensions;
using Pawz.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModule(new CoreModule());
builder.Services.AddModule(new AuthModule());
builder.Services.AddModule(new DataModule(builder.Configuration));

var app = builder.Build();

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