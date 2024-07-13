using BrazilCities.Application.Repositories;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public abstract class RepositoryBase<T>(AppDbContext appDbContext) : IRepository<T>
    where T : class 
{
    private readonly DbSet<T> _dbSet = appDbContext.Set<T>();
    public async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken: cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await appDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        if (entity == null)
        {
            return false;
        }
        _dbSet.Remove(entity);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}