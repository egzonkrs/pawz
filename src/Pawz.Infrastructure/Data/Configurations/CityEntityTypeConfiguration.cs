using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pawz.Domain.Entities;

namespace Pawz.Infrastructure.Data.Configurations;

public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasOne(c => c.Country)
            .WithMany(co => co.Cities)
            .HasForeignKey(c => c.CountryId);
    }
}
