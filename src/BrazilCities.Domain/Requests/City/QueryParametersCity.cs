namespace BrazilCities.Domain.Requests.City;

public class QueryParametersCity : QueryParametersBase
{
    public string? Name { get; set; }
    public string? StateAcronym { get; set; }
}