using BrazilCities.Application.Repositories;
using BrazilCities.Application.Services;
using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Persistence.Repositories;

namespace BrazilCities.Api.Configurations;

public static class DependenciesInjections
{
    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IStateRepository, StateRepository>();
        services.AddSingleton<ICityRepository, CityRepository>();
    }
    
    internal static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStateService, StateService>();
        services.AddScoped<ICityService, CityService>();
    }
}