using BrazilCities.Application.Caching;
using BrazilCities.Application.Repositories;
using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Extensions;
using BrazilCities.Domain.Requests.City;
using BrazilCities.Domain.Responses;
using BrazilCities.Domain.Responses.City;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Application.Services;

public class CityService(ICityRepository cityRepository, IStateRepository stateRepository, ICacheService cacheService) : ICityService
{
    public async Task<PagedListResponse<CityResponse?>> GetAllAsync(QueryParametersCity queryParametersCity, CancellationToken cancellationToken)
    {
        return await cityRepository.FindAllQueryParametersAsync(queryParametersCity, cancellationToken);
    }
    
    public async Task<CityResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        CityEntity? city = await cacheService.GetAsync($"city:{id}", 
            async () => await cityRepository.FindByIdAsync(id, cancellationToken),  cancellationToken);
        
        return city.ToCityAndStateResponse();
    }

    public async Task<CityResponse?> CreateAsync(CityPostRequest cityPostRequest, CancellationToken cancellationToken)
    {
        var state = await stateRepository.FindByAcronymAsync(cityPostRequest.StateAcronym, cancellationToken);

        if (state is null)
        {
            throw new Exception($"State with acronym {cityPostRequest.StateAcronym} not found.");
        }

        var cityEntity = new CityEntity
        {
            Name = cityPostRequest.Name,
            StateId = state.Id
        };

        var city = await cityRepository.CreateAsync(cityEntity, cancellationToken);

        return city.ToCityResponse();
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