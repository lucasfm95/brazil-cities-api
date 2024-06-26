using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<StateEntity> States { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactConfiguration).Assembly);
    }
}