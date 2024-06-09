using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public sealed class CityRepository(DbContext context) : RepositoryBase<CityEntity>(context), ICityRepository;