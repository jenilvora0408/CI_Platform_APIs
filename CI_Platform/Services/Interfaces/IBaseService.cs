using System.Linq.Expressions;

namespace Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);

        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    }
}
