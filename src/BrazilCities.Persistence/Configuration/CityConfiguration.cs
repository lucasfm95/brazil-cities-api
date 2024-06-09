using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrazilCities.Persistence.Configuration;

public class CityConfiguration : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.HasKey(city => city.Id);
        builder.Property(city => city.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(city => city.State)
            .IsRequired();
    }
}