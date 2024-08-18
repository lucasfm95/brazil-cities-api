using BrazilCities.Domain.Entities;

namespace BrazilCities.Application.Repositories;

public interface ICityRepository : IRepository<CityEntity>
{
    Task<List<CityEntity>> FindAllQueryParams(string? name, string? stateAcronym, CancellationToken cancellationToken);
}