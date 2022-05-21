namespace Application.Common.Interfaces.Repository;

public interface IRepository<T, TId> where T : class
{
    Task<T> GetByIdAsync(object id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task Save();
}