namespace BrazilCities.Domain.Entities;

public class CityEntity : BaseEntity
{
    public string? Name { get; set; }
    public int StateId { get; set; }
    public StateEntity? State { get; set; }
}