using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations;

public class PetEntityTypeConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder
            .HasOne(p => p.Species)
            .WithMany(s => s.Pets)
            .HasForeignKey(p => p.SpeciesId);

        builder
            .HasOne(p => p.Breed)
            .WithMany(b => b.Pets)
            .HasForeignKey(p => p.BreedId);

        builder
            .HasOne(p => p.Location)
            .WithMany(l => l.Pets)
            .HasForeignKey(p => p.LocationId);
    }
}
