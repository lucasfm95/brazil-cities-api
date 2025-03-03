using BrazilCities.Application.Repositories;
using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.State;
using BrazilCities.Domain.Responses;
using BrazilCities.Domain.Responses.State;

namespace BrazilCities.Application.Services;

public sealed class StateService(IStateRepository stateRepository) : IStateService
{
    public async Task<PagedListResponse<StateResponse>> GetAllAsync(QueryParametersState queryParametersState, CancellationToken cancellationToken)
    {
        return await stateRepository.FindAllQueryParametersAsync(queryParametersState, cancellationToken);
    }

    public async Task<StateEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await stateRepository.FindByIdAsync(id, cancellationToken);
    }

    public async Task<StateEntity?> GetByAcronymAsync(string acronym, CancellationToken cancellationToken)
    {
        return await stateRepository.FindByAcronymAsync(acronym, cancellationToken);
    }

    public async Task<StateEntity> CreateAsync(StatePostRequest statePostRequest, CancellationToken cancellationToken)
    {
        var stateEntity = new StateEntity
        {
            Name = statePostRequest.Name,
            StateAcronym = statePostRequest.StateAcronym
        };
        
        return await stateRepository.CreateAsync(stateEntity, cancellationToken);
    }

    public async Task<bool> UpdateAsync(int id, StatePutRequest statePutRequest, CancellationToken cancellationToken)
    {
        var state = await GetByIdAsync(id, cancellationToken);
        
        if (state is null)
        {
            throw new ($"State with id {id} not found.");
        }
        
        var stateEntity = new StateEntity
        {
            Id = id,
            Name = statePutRequest.Name,
            StateAcronym = statePutRequest.StateAcronym
        };
        
        return await stateRepository.UpdateAsync(stateEntity, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await stateRepository.DeleteAsync(id, cancellationToken);
    }
}