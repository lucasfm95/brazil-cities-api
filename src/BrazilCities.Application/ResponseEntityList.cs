using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Application;

public class ResponseEntityList<T>
{
    private ResponseEntityList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public List<T> Items { get; }
    
    public int Page { get; }
    
    public int PageSize { get; }
    
    public int TotalCount { get; }
    
    public bool HasNextPage => Page * PageSize < TotalCount;
    
    public bool HasPreviousPage => Page > 1;
    
    public static async Task<ResponseEntityList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    
        return new(items, page, pageSize, totalCount);
    }
}