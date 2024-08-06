using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System.Reflection;

namespace Pawz.Infrastructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Adoption> Adoptions { get; set; }
    public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetImage> PetImages { get; set; }
    public DbSet<Species> Species { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // To-do:
        // One-to-Many: A user can post multiple pets
        // One-to-Many: A user can make multiple adoption requests
        // One-to-One: An adoption has one payment
        // One-to-Many: A user can make multiple payments

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
