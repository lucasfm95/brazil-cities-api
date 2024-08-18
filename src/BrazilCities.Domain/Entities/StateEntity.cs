using System.Text.Json.Serialization;

namespace BrazilCities.Domain.Entities;

public class StateEntity : BaseEntity
{
    public string? StateAcronym { get; set; }
    public string? Name { get; set; }
    public IEnumerable<CityEntity>? Cities { get; set; }
}