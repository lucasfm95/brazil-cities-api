using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.State;

namespace BrazilCities.Application.Services.Interfaces;

public interface IStateService
{
    Task<IEnumerable<StateEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<StateEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<StateEntity> GetByAcronymAsync(string acronym, CancellationToken cancellationToken);
    Task<StateEntity> CreateAsync(StatePostRequest statePostRequest, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, StatePutRequest statePutRequest, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}