using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.State;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Application.Services.Interfaces;

public interface IStateService
{
    Task<IEnumerable<StateResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<StateEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<StateEntity> GetByAcronymAsync(string acronym, CancellationToken cancellationToken);
    Task<StateEntity> CreateAsync(StatePostRequest statePostRequest, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, StatePutRequest statePutRequest, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}