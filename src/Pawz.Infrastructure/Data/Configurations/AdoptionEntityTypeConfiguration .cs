using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations;

public class AdoptionEntityTypeConfiguration : IEntityTypeConfiguration<Adoption>
{
    public void Configure(EntityTypeBuilder<Adoption> builder)
    {
        builder
            .HasOne(a => a.AdoptionRequest)
            .WithOne(ar => ar.Adoption)
            .HasForeignKey<Adoption>(a => a.AdoptionRequestId);

        builder
            .Property(a => a.AdoptionFee)
            .HasColumnType("decimal(18,2)");
    }
}
