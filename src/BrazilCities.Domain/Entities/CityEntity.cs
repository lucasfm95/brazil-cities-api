namespace BrazilCities.Domain.Entities;

public class CityEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public StateEntity? State { get; set; }
}