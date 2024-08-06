using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;

namespace Pawz.Infrastructure.Data.Configurations
{
    public class AdoptionRequestEntityTypeConfiguration : IEntityTypeConfiguration<AdoptionRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AdoptionRequest> builder)
        {
            builder
                .HasOne(ar => ar.Pet)
                .WithMany(p => p.AdoptionRequests)
                .HasForeignKey(ar => ar.PetId);
        }
    }
}