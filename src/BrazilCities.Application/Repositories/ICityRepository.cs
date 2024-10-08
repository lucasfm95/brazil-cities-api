using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;

namespace BrazilCities.Application.Repositories;

public interface ICityRepository : IRepository<CityEntity>
{
    Task<List<CityEntity>> FindAllQueryParametersAsync(QueryParametersCity queryParametersCity, CancellationToken cancellationToken);
}