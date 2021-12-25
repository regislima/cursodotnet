using System.Threading.Tasks;
using api.Domain.Models;
using api.Domain.Repositories;
using api.Domain.Services;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> FirstOrDefaultAsync(string login, string password)
        {
            return await _userRepository.FirstOrDefaultAsync(login, password);
        }
    }
}