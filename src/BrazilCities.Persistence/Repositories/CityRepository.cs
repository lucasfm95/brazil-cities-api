using System.Linq.Expressions;
using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Requests.City;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public sealed class CityRepository(AppDbContext appDbContext) : RepositoryBase<CityEntity>(appDbContext), ICityRepository
{
    public async Task<List<CityEntity>> FindAllQueryParams(QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var query = DbSet.Include("State");

        if (!string.IsNullOrWhiteSpace(queryParameters.Name))
            query = FilterByName(queryParameters, query);

        if (!string.IsNullOrWhiteSpace(queryParameters.StateAcronym))
            query = FilterByStateAcronym(queryParameters, query);

        query = Sort(queryParameters, query);

        query = Pagination(queryParameters, query);

        return await query.ToListAsync(cancellationToken);
    }

    private static Expression<Func<CityEntity, object>> GetSortProperty(QueryParameters queryParameters) =>
        queryParameters.SortColumn?.ToLower() switch
        {
            "id" => city => city.Id,
            "name" => city => city.Name ?? string.Empty,
            "state_acronym" => city => city.State!.StateAcronym ?? string.Empty,
            _ => city => city.Id
        };

    private static IQueryable<CityEntity> Sort(QueryParameters queryParameters, IQueryable<CityEntity> query) =>
        queryParameters.SortOrder?.ToLower() == "desc"
            ? query.OrderByDescending(GetSortProperty(queryParameters))
            : query.OrderBy(GetSortProperty(queryParameters));

    private static IQueryable<CityEntity> Pagination(QueryParameters queryParameters, IQueryable<CityEntity> query) =>
        query.Skip((queryParameters.Page - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);

    private static IQueryable<CityEntity> FilterByName(QueryParameters queryParameters, IQueryable<CityEntity> query) =>
        query.Where(city => city.Name!.ToLower().Contains(queryParameters.Name!.ToLower()));

    private static IQueryable<CityEntity> FilterByStateAcronym(QueryParameters queryParameters, IQueryable<CityEntity> query) =>
        query.Where(city => city.State!.StateAcronym!.ToLower().Contains(queryParameters.StateAcronym!.ToLower()));
}