using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Responses.City;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Domain.Extensions;

public static class CityEntityExtensions
{
    public static CityResponse? ToCityAndStateResponse(this CityEntity? city)
    {
        CityResponse? cityResponse = null;
        
        if (city is not null)
        {
            cityResponse = new CityResponse
            {
                Id = city.Id,
                Name = city.Name!,
                CreatedAt = city.CreatedAt,
                UpdatedAt = city.UpdatedAt
            };
    
            if (city.State is not null)
            {
                cityResponse.State = new StateResponse
                {
                    Id = city.State.Id,
                    Name = city.State.Name,
                    Acronym = city.State.StateAcronym,
                    CreatedAt = city.State.CreatedAt,
                    UpdatedAt = city.State.UpdatedAt
                };
            }
        }
        return cityResponse;
    }
    
    public static CityResponse ToCityResponse(this CityEntity city)
    {
        return new CityResponse
        {
            Id = city.Id,
            Name = city.Name!,
            CreatedAt = city.CreatedAt,
            UpdatedAt = city.UpdatedAt
        };
    }
}