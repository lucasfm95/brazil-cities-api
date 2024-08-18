using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Domain.Responses.City;

public class CityResponse : BaseResponse
{
    public string? Name { get; set; }
    public StateResponse? State { get; set; }
}