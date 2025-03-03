using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;
using BrazilCities.Domain.Responses;
using BrazilCities.Domain.Responses.City;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Application.Repositories;

public interface ICityRepository : IRepository<CityEntity>
{
    Task<PagedListResponse<CityResponse?>> FindAllQueryParametersAsync(QueryParametersCity queryParametersCity, CancellationToken cancellationToken);
}