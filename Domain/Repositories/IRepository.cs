using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Domain.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> ListAsync();
        Task AddAsync(T entity);
        Task<T> FindByIdAsync(int id);
        void update(T entity);
        void Remove(T entity);
    }
}