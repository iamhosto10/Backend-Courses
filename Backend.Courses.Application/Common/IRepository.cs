using System.Linq.Expressions;

namespace Backend.Courses.Application.Common;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(string id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);
}
