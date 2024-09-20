using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations;

public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
           .Property(n => n.SenderId);

        builder
          .Property(n => n.RecipientId);

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
            .HasIndex(n => n.IsDeleted);

        builder
            .HasIndex(n => n.CreatedAt);
    }
}
