using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrazilCities.Persistence.Configuration;

public class CityConfiguration : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.ToTable("Cities");
        builder.HasKey(city => city.Id);
        builder.Property(city => city.Id)
            .HasColumnType("int")
            .UseIdentityColumn();
        builder.Property(city => city.Name)
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(city => city.StateId)
            .HasColumnType("int")
            .IsRequired();
        builder.Property(state => state.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");
        builder.Property(state => state.UpdatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");
        builder.HasOne(city => city.State)
            .WithMany(state => state.Cities)
            .HasPrincipalKey(state => state.Id);
    }
}