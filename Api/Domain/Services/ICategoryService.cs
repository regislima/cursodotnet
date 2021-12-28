using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<IEnumerable<Category>> FindByNameAsync(string name);
    }
}