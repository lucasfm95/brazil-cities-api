namespace BrazilCities.Domain.Requests.City;

public class CityPostRequest
{
    public required string Name { get; set; }
    public required string StateAcronym { get; set; }
}