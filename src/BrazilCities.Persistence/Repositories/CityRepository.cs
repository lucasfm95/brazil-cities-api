using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public sealed class CityRepository(AppDbContext appDbContext) : RepositoryBase<CityEntity>(appDbContext), ICityRepository
{
    public async Task<List<CityEntity>> FindAllQueryParams(string? name, string? stateAcronym, CancellationToken cancellationToken)
    {
        IQueryable<CityEntity> query = DbSet;
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(city => city.Name!.ToLower().Contains(name.ToLower()));
        }
        
        if (!string.IsNullOrWhiteSpace(stateAcronym))
        {
            query = query.Where(city => city.State!.StateAcronym!.ToLower().Contains(stateAcronym.ToLower()));
        }
        
        query = query.Include("State");
        
        return await query.ToListAsync(cancellationToken);
    }
}