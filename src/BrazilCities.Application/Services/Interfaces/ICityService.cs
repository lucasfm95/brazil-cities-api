using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;
using BrazilCities.Domain.Responses.City;

namespace BrazilCities.Application.Services.Interfaces;

public interface ICityService
{
    Task<IEnumerable<CityResponse?>> GetAllAsync(string? name, string? stateAcronym, CancellationToken cancellationToken);
    Task<CityResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<CityResponse?> CreateAsync(CityPostRequest cityPostRequest, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CityPutRequest cityPutRequest, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}