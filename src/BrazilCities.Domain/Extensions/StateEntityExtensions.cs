using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Domain.Extensions;

public static class StateEntityExtensions
{
    public static StateResponse ToStateResponse(this StateEntity state)
    {
        return new StateResponse
        {
            Id = state.Id,
            Name = state.Name,
            Acronym = state.StateAcronym,
            CreatedAt = state.CreatedAt,
            UpdatedAt = state.UpdatedAt,
        };
    }
}