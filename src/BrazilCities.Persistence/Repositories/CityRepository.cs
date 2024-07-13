using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Persistence.Context;

namespace BrazilCities.Persistence.Repositories;

public sealed class CityRepository(AppDbContext appDbContext) : RepositoryBase<CityEntity>(appDbContext), ICityRepository;