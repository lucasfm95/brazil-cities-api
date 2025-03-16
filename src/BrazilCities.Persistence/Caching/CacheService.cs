using System.Text.Json;
using BrazilCities.Application.Caching;
using StackExchange.Redis;

namespace BrazilCities.Persistence.Caching;

public class CacheService(IConnectionMultiplexer cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var db = cache.GetDatabase();
        
        var value = await db.StringGetAsync(key);
        
        return string.IsNullOrEmpty( value ) ? null : JsonSerializer.Deserialize<T>(value! );
    }

    public async Task<T?> GetAsync<T>(string key, Func<Task<T?>> factory, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);
        
        if (cachedValue is not null)
        {
            return cachedValue;
        }
        
        cachedValue = await factory();

        if (cachedValue is null)
        {
            return null;
        }
        
        await SetAsync(key, cachedValue, cancellationToken);
        
        return cachedValue;
    }

    public async Task<bool> SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        var db = cache.GetDatabase();

        var serializedValue = JsonSerializer.Serialize(value);
        
        return await db.StringSetAsync(key, serializedValue);
    }
    
    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var db = cache.GetDatabase();
        
        return await db.KeyDeleteAsync(key);
    }
}