using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;

namespace BrazilCities.Application.Repositories;

public interface ICityRepository : IRepository<CityEntity>
{
    Task<ResponseEntityList<CityEntity>> FindAllQueryParametersAsync(QueryParametersCity queryParametersCity, CancellationToken cancellationToken);
}