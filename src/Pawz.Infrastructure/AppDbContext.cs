using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure;

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
        // One-to-Many: A user can post multiple pets
        // One-to-Many: A user can make multiple adoption requests
        // One-to-One: An adoption has one payment
        // One-to-Many: A user can make multiple payments

        // One-to-Many: A species can have multiple pets
        modelBuilder.Entity<Pet>()
            .HasOne(p => p.Species)
            .WithMany(s => s.Pets)
            .HasForeignKey(p => p.SpeciesId);

        // One-to-Many: A breed can have multiple pets
        modelBuilder.Entity<Pet>()
            .HasOne(p => p.Breed)
            .WithMany(b => b.Pets)
            .HasForeignKey(p => p.BreedId);

        // One-to-Many: A species can have multiple breeds
        modelBuilder.Entity<Breed>()
            .HasOne(b => b.Species)
            .WithMany(s => s.Breeds)
            .HasForeignKey(b => b.SpeciesId);

        // One-to-Many: A pet can have multiple adoption requests
        modelBuilder.Entity<AdoptionRequest>()
            .HasOne(ar => ar.Pet)
            .WithMany(p => p.AdoptionRequests)
            .HasForeignKey(ar => ar.PetId);

        // One-to-One: An approved adoption request leads to one adoption
        modelBuilder.Entity<Adoption>()
            .HasOne(a => a.AdoptionRequest)
            .WithOne(ar => ar.Adoption)
            .HasForeignKey<Adoption>(a => a.AdoptionRequestId);

        // One-to-Many: A location can have multiple pets
        modelBuilder.Entity<Pet>()
            .HasOne(p => p.Location)
            .WithMany(l => l.Pets)
            .HasForeignKey(p => p.LocationId);

        // One-to-Many: A pet can have multiple images
        modelBuilder.Entity<PetImage>()
            .HasOne(pi => pi.Pet)
            .WithMany(p => p.PetImages)
            .HasForeignKey(pi => pi.PetId);

        base.OnModelCreating(modelBuilder);
    }
}
