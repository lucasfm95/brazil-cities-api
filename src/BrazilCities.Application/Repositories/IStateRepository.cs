using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.State;

namespace BrazilCities.Application.Repositories;

public interface IStateRepository : IRepository<StateEntity>
{
    Task<List<StateEntity>> FindAllQueryParametersAsync(QueryParametersState queryParametersState, CancellationToken cancellationToken);
    Task<StateEntity?> FindByAcronymAsync(string acronym, CancellationToken cancellationToken);
}