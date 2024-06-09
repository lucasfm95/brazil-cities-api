using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;

namespace BrazilCities.Application.Services.Interfaces;

public interface ICityService
{
    Task<IEnumerable<CityEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<CityEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<CityEntity> CreateAsync(CityPostRequest cityPostRequest, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CityPutRequest city, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}