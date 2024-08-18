using BrazilCities.Domain.Entities;

namespace BrazilCities.Application.Repositories;

public interface ICityRepository : IRepository<CityEntity>
{
    Task<IEnumerable<CityEntity>> FindAllCityWithState(CancellationToken cancellationToken);
}