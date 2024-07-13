using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<StateEntity> States { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING_DB_POSTGRES"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}