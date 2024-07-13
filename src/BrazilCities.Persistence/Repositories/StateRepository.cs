using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Persistence.Context;

namespace BrazilCities.Persistence.Repositories;

public sealed class StateRepository(AppDbContext appDbContext) : RepositoryBase<StateEntity>(appDbContext), IStateRepository;