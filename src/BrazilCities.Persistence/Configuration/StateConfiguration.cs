using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrazilCities.Persistence.Configuration;

public class StateConfiguration : IEntityTypeConfiguration<StateEntity>
{
    public void Configure(EntityTypeBuilder<StateEntity> builder)
    {
        builder.ToTable("States");
        builder.HasKey(state => state.Id);
        builder.Property(state => state.Id)
            .HasColumnType("int")
            .UseIdentityColumn();
        builder.Property(state => state.StateAcronym)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(2);
        builder.Property(state => state.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(200);
        builder.Property(state => state.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");
        builder.Property(state => state.UpdatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");
    }
}