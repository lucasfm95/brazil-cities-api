using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Domain.Responses;

public class PagedListResponse<T>
{
    private PagedListResponse(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public List<T>? Items { get; }
    
    public int Page { get; }
    
    public int PageSize { get; }
    
    public int TotalCount { get; }
    
    public bool HasNextPage => Page * PageSize < TotalCount;
    
    public bool HasPreviousPage => Page > 1;
    
    public static async Task<PagedListResponse<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    
        return new PagedListResponse<T>(items, page, pageSize, totalCount);
    }
}