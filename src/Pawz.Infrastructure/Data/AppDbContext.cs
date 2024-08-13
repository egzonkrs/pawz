using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Reflection;

namespace Pawz.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Adoption> Adoptions { get; set; }
    public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetImage> PetImages { get; set; }
    public DbSet<Species> Species { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-O50J359;Database=PetAdoption;Trusted_Connection=True; TrustServerCertificate=true;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define precision and scale for decimal properties
        modelBuilder.Entity<Adoption>()
            .Property(a => a.AdoptionFee)
            .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

        modelBuilder.Entity<Pet>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

        // Apply configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Call the base method
        base.OnModelCreating(modelBuilder);
    }

}
