using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> FirstOrDefaultAsync(string login, string password);
    }
}