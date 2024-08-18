using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public sealed class CityRepository(AppDbContext appDbContext) : RepositoryBase<CityEntity>(appDbContext), ICityRepository
{
    public async Task<IEnumerable<CityEntity>> FindAllCityWithState(CancellationToken cancellationToken)
    {
        return await DbSet.Include("State").ToListAsync(cancellationToken);
    }
}