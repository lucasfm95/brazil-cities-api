using BrazilCities.Application.Repositories;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public abstract class RepositoryBase<T>(AppDbContext appDbContext) : IRepository<T> where T : class
{
    protected readonly DbSet<T> DbSet = appDbContext.Set<T>();
    public async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await DbSet.AddAsync(entity, cancellationToken: cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await appDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FindAsync(id, cancellationToken);
        if (entity == null)
        {
            return false;
        }
        DbSet.Remove(entity);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}