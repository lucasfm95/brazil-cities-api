namespace BrazilCities.Domain.Requests.City;

public class QueryParameters
{
    public string? Name { get; set; }
    public string? StateAcronym { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}