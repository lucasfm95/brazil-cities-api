using BrazilCities.Domain.Responses.City;

namespace BrazilCities.Domain.Responses.State;

public class StateResponse : BaseResponse
{
    public string? Acronym { get; set; }
    public string? Name { get; set; }
}