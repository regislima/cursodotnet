using System.Collections.Generic;
using System.Threading.Tasks;
using api.Util.Comunication;

namespace api.Domain.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> ListAsync();
        Task<Response<T>> SaveAsync(T entity);
        Task<Response<T>> UpdateAsync(int id, T entity);
        Task<Response<T>> DeleteAsync(int id);
        Task<T> FindByIdAsync(int id);
    }
}