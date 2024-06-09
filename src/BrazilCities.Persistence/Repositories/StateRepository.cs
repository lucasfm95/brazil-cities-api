using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BrazilCities.Persistence.Repositories;

public sealed class StateRepository(DbContext dbContext) : RepositoryBase<StateEntity>(dbContext), IStateRepository;