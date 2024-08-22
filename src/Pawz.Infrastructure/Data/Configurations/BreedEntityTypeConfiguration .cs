using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations;

public class BreedEntityTypeConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder
           .HasOne(b => b.Species)
           .WithMany(s => s.Breeds)
           .HasForeignKey(b => b.SpeciesId);
    }
}
