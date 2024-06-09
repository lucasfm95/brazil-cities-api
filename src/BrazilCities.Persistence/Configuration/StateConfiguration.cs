using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrazilCities.Persistence.Configuration;

public class StateConfiguration : IEntityTypeConfiguration<StateEntity>
{
    public void Configure(EntityTypeBuilder<StateEntity> builder)
    {
        builder.HasKey(state => state.Id);
        builder.Property(state => state.StateAcronym)
            .IsRequired()
            .HasMaxLength(2);
        builder.Property(state => state.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}