using System.Linq.Expressions;
using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Extensions;
using BrazilCities.Domain.Requests.State;
using BrazilCities.Domain.Responses;
using BrazilCities.Domain.Responses.State;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public sealed class StateRepository(AppDbContext appDbContext) : RepositoryBase<StateEntity>(appDbContext), IStateRepository
{
    public async Task<PagedListResponse<StateResponse>> FindAllQueryParametersAsync(QueryParametersState queryParametersState, CancellationToken cancellationToken)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParametersState.Name))
            query = FilterByName(queryParametersState, query);

        query = Sort(queryParametersState, query);
        
        var queryMapped = query
            .Select(state => state.ToStateResponse());
        
        return await PagedListResponse<StateResponse>.CreateAsync(queryMapped, queryParametersState.Page, queryParametersState.PageSize);
    }

    public async Task<StateEntity?> FindByAcronymAsync(string acronym, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(state => state.StateAcronym!.Equals(acronym, StringComparison.InvariantCultureIgnoreCase), cancellationToken);
    }
    
    private static Expression<Func<StateEntity, object>> GetSortProperty(QueryParametersState queryParametersState) =>
        queryParametersState.SortColumn?.ToLower() switch
        {
            "id" => state => state.Id,
            "name" => state => state.Name ?? string.Empty,
            "acronym" => state => state.StateAcronym ?? string.Empty,
            _ => state => state.Id
        };

    private static IQueryable<StateEntity> Sort(QueryParametersState queryParametersState, IQueryable<StateEntity> query) =>
        queryParametersState.SortOrder?.ToLower() == "desc"
            ? query.OrderByDescending(GetSortProperty(queryParametersState))
            : query.OrderBy(GetSortProperty(queryParametersState));

    private static IQueryable<StateEntity> Pagination(QueryParametersState queryParametersState, IQueryable<StateEntity> query) =>
        query.Skip((queryParametersState.Page - 1) * queryParametersState.PageSize).Take(queryParametersState.PageSize);

    private static IQueryable<StateEntity> FilterByName(QueryParametersState queryParametersState, IQueryable<StateEntity> query) =>
        query.Where(state => state.Name!.ToLower().Contains(queryParametersState.Name!.ToLower()));
}