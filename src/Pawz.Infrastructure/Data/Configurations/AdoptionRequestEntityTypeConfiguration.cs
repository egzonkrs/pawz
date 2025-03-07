using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations;

public class AdoptionRequestEntityTypeConfiguration : IEntityTypeConfiguration<AdoptionRequest>
{
    public void Configure(EntityTypeBuilder<AdoptionRequest> builder)
    {
        builder
            .HasOne(ar => ar.Pet)
            .WithMany(p => p.AdoptionRequests)
            .HasForeignKey(ar => ar.PetId);

        builder
            .HasOne(ar => ar.Location)
            .WithMany(l => l.AdoptionRequests)
            .HasForeignKey(ar => ar.LocationId);
    }
}
