namespace BrazilCities.Domain.Requests.State;

public class StatePutRequest
{
    public string? StateAcronym { get; set; }
    public string? Name { get; set; }
}