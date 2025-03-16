namespace BrazilCities.Application.Services.Interfaces;

public interface IDistributedCachingService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken);
    Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken);
}