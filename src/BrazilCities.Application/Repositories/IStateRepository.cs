using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.State;
using BrazilCities.Domain.Responses;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Application.Repositories;

public interface IStateRepository : IRepository<StateEntity>
{
    Task<PagedListResponse<StateResponse>> FindAllQueryParametersAsync(QueryParametersState queryParametersState, CancellationToken cancellationToken);
    Task<StateEntity?> FindByAcronymAsync(string acronym, CancellationToken cancellationToken);
}