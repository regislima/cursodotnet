using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> FindByNameAsync(string name);
    }
}