using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations;

public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
            .HasKey(n => n.Id);

        builder
            .Property(n => n.SenderId)
            .IsRequired();

        builder
            .Property(n => n.RecipientId)
            .IsRequired();

        builder
            .HasOne(n => n.Sender)
            .WithMany()
            .HasForeignKey(n => n.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(n => n.Recipient)
            .WithMany()
            .HasForeignKey(n => n.RecipientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasIndex(n => n.RecipientId);

        builder
            .HasIndex(n => n.SenderId);

        builder
            .HasIndex(n => new { n.IsDeleted, n.CreatedAt });
    }
}
