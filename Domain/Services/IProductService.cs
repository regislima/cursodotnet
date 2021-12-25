using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Services
{
    public interface IProductService : IService<Product>
    {
        Task<IEnumerable<Product>> FindByNameAsync(string name);
    }
}