namespace BrazilCities.Domain.Requests;

public class QueryParametersBase
{
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}