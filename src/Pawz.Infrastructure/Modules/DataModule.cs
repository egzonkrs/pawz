using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawz.Application.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Modules;
public class DataModule : IModule
{
    public void Load(IServiceCollection services)
    {
        var connectionString = services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));
    }
}
