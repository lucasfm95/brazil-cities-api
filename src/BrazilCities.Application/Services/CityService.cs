using BrazilCities.Application.Repositories;
using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;
using BrazilCities.Domain.Responses.City;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Application.Services;

public class CityService(ICityRepository cityRepository, IStateRepository stateRepository) : ICityService
{
    public async Task<IEnumerable<CityResponse?>> GetAllAsync(QueryParametersCity queryParametersCity, CancellationToken cancellationToken)
    {
        var cities = await cityRepository.FindAllQueryParametersAsync(queryParametersCity, cancellationToken);
        
        var cityResponses = cities.Select(ToCityResponse);
        
        return cityResponses;
    }
    
    public async Task<CityResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var city = await cityRepository.FindByIdAsync(id, cancellationToken);
        
        return ToCityResponse(city);
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

        return ToCityResponse(city);
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
    
    private CityResponse? ToCityResponse(CityEntity? cityEntity)
    {
        if (cityEntity is { State: not null })
            return new CityResponse
            {
                Id = cityEntity.Id,
                Name = cityEntity.Name,
                CreatedAt = cityEntity.CreatedAt,
                UpdatedAt = cityEntity.UpdatedAt,
                State = new StateResponse
                {
                    Id = cityEntity.State.Id,
                    Name = cityEntity.State.Name,
                    Acronym = cityEntity.State.StateAcronym,
                    CreatedAt = cityEntity.State.CreatedAt,
                    UpdatedAt = cityEntity.State.UpdatedAt
                }
            };
        return null;
    }
}