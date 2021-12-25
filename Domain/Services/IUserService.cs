using System.Threading.Tasks;
using api.Domain.Models;

namespace api.Domain.Services
{
    public interface IUserService
    {
        Task<User> FirstOrDefaultAsync(string login, string password);
    }
}