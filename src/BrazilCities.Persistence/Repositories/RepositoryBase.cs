using BrazilCities.Application.Repositories;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public abstract class RepositoryBase<T>(AppDbContext appDbContext) : IRepository<T>
    where T : class
{
    public async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await appDbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await appDbContext.Set<T>().AddAsync(entity, cancellationToken: cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        appDbContext.Set<T>().Update(entity);
        await appDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await appDbContext.Set<T>().FindAsync(id, cancellationToken);
        if (entity == null)
        {
            return false;
        }
        appDbContext.Set<T>().Remove(entity);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}