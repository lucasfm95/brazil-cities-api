using BrazilCities.Application.Repositories;
using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;

namespace BrazilCities.Application.Services;

public class CityService(ICityRepository cityRepository) : ICityService
{
    public async Task<IEnumerable<CityEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await cityRepository.FindAllAsync(cancellationToken);
    }
    
    public async Task<CityEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await cityRepository.FindByIdAsync(id, cancellationToken);
    }
    
    public async Task<CityEntity> CreateAsync(CityPostRequest cityPostRequest, CancellationToken cancellationToken)
    {
        var city = new CityEntity
        {
            Name = cityPostRequest.Name,
        };
        
        return await cityRepository.CreateAsync(city, cancellationToken);
    }
    
    public async Task<bool> UpdateAsync(CityPutRequest cityPutRequest, CancellationToken cancellationToken)
    {
        var city = new CityEntity
        {
            Name = cityPutRequest.Name,
        };
        
        return await cityRepository.UpdateAsync(city, cancellationToken);
    }
    
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await cityRepository.DeleteAsync(id, cancellationToken);
    }
}