namespace BrazilCities.Domain.Requests.State;

public class StatePostRequest
{
    public required string StateAcronym { get; set; }
    public required string Name { get; set; }
}