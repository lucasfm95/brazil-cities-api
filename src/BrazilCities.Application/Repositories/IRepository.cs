namespace BrazilCities.Application.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default);
    Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);    
}