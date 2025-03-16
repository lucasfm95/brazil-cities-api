using BrazilCities.Application.Caching;
using BrazilCities.Application.Repositories;
using BrazilCities.Application.Services;
using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Persistence.Caching;
using BrazilCities.Persistence.Repositories;
using StackExchange.Redis;

namespace BrazilCities.Api.Configurations;

public static class DependenciesInjections
{
    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
    }
    
    internal static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStateService, StateService>();
        services.AddScoped<ICityService, CityService>();
        
        var redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("CONNECTION_STRING_REDIS")!);
        services.AddSingleton<IConnectionMultiplexer>(redis);
        services.AddSingleton<ICacheService, CacheService>();
    }
}