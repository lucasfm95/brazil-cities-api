using BrazilCities.Domain.Entities;

namespace BrazilCities.Application.Repositories;

public interface IStateRepository : IRepository<StateEntity>
{
    Task<StateEntity?> FindByAcronymAsync(string acronym, CancellationToken cancellationToken);
}