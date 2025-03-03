using System.Linq.Expressions;
using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Domain.Extensions;
using BrazilCities.Domain.Requests.City;
using BrazilCities.Domain.Responses;
using BrazilCities.Domain.Responses.City;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public sealed class CityRepository(AppDbContext appDbContext) : RepositoryBase<CityEntity>(appDbContext), ICityRepository
{
    public async Task<PagedListResponse<CityResponse>> FindAllQueryParametersAsync(QueryParametersCity queryParametersCity,
        CancellationToken cancellationToken)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParametersCity.Name))
            query = FilterByName(queryParametersCity, query);

        if (!string.IsNullOrWhiteSpace(queryParametersCity.StateAcronym))
            query = FilterByStateAcronym(queryParametersCity, query);

        query = Sort(queryParametersCity, query);

        var queryInclude = query
            .Include(city => city.State)
            .Select(city => city.ToCityAndStateResponse());

        return await PagedListResponse<CityResponse>.CreateAsync(queryInclude, queryParametersCity.Page, queryParametersCity.PageSize);
    }

    private static Expression<Func<CityEntity, object>> GetSortProperty(QueryParametersCity queryParametersCity) =>
        queryParametersCity.SortColumn?.ToLower() switch
        {
            "id" => city => city.Id,
            "name" => city => city.Name ?? string.Empty,
            "state_acronym" => city => city.State!.StateAcronym ?? string.Empty,
            _ => city => city.Id
        };

    private static IQueryable<CityEntity> Sort(QueryParametersCity queryParametersCity, IQueryable<CityEntity> query) =>
        queryParametersCity.SortOrder?.ToLower() == "desc"
            ? query.OrderByDescending(GetSortProperty(queryParametersCity))
            : query.OrderBy(GetSortProperty(queryParametersCity));

    private static IQueryable<CityEntity> Pagination(QueryParametersCity queryParametersCity, IQueryable<CityEntity> query) =>
        query.Skip((queryParametersCity.Page - 1) * queryParametersCity.PageSize).Take(queryParametersCity.PageSize);

    private static IQueryable<CityEntity> FilterByName(QueryParametersCity queryParametersCity, IQueryable<CityEntity> query) =>
        query.Where(city => city.Name!.ToLower().Contains(queryParametersCity.Name!.ToLower()));

    private static IQueryable<CityEntity> FilterByStateAcronym(QueryParametersCity queryParametersCity, IQueryable<CityEntity> query) =>
        query.Where(city => city.State!.StateAcronym!.ToLower().Contains(queryParametersCity.StateAcronym!.ToLower()));
}