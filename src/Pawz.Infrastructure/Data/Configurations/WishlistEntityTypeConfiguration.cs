using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;
using System.Collections.Generic;

namespace Pawz.Infrastructure.Data.Configurations;

public class WishlistEntityTypeConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.HasKey(w => w.Id);

        builder.HasOne(w => w.User)
            .WithOne(u => u.Wishlist)
            .HasForeignKey<Wishlist>(w => w.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(w => w.Pets)
            .WithMany(p => p.WishlistedByUsers)
            .UsingEntity<Dictionary<string, object>>(
                "WishlistPets",
                j => j
                    .HasOne<Pet>()
                    .WithMany()
                    .HasForeignKey("PetId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Wishlist>()
                    .WithMany()
                    .HasForeignKey("WishlistId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("WishlistId", "PetId");
                    j.ToTable("WishlistPets");
                });
    }
}
