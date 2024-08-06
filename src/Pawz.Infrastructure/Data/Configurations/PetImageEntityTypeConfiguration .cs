using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations
{
    public class PetImageEntityTypeConfiguration : IEntityTypeConfiguration<PetImage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PetImage> builder)
        {
            builder
                .HasOne(pi => pi.Pet)
                .WithMany(p => p.PetImages)
                .HasForeignKey(pi => pi.PetId);
        }
    }
}