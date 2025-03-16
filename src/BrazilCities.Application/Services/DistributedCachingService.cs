using System.Text.Json;
using BrazilCities.Application.Services.Interfaces;
using StackExchange.Redis;

namespace BrazilCities.Application.Services;

public class DistributedCachingService(IConnectionMultiplexer cache) : IDistributedCachingService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
    {
        var db = cache.GetDatabase();
        
        var value = await db.StringGetAsync(key);
        
        if (string.IsNullOrEmpty( value ))
        {
            return default;
        }
        else
        {
            return JsonSerializer.Deserialize<T>(value! );
        }
    }
    
    public async Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken)
    {
        var serializedValue = JsonSerializer.Serialize(value);

        var db = cache.GetDatabase();
        
        await db.StringSetAsync(key, serializedValue);

        return true;
    }
}