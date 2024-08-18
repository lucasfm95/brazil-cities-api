namespace BrazilCities.Domain.Responses;

public class BaseResponse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}