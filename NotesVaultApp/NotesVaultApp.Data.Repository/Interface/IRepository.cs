using System.Linq.Expressions;

namespace NotesVaultApp.Data.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAttached();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
