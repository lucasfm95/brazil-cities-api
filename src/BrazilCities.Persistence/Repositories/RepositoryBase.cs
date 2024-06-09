using BrazilCities.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public abstract class RepositoryBase<T>(DbContext context) : IRepository<T>
    where T : class
{
    public async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken: cancellationToken);
        await context.SaveChangesAsync(cancellationToken: cancellationToken);
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync(cancellationToken: cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Set<T>().FindAsync(id, cancellationToken);
        if (entity == null)
        {
            return false;
        }
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}