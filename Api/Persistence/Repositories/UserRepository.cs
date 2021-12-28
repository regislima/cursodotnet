using System.Threading.Tasks;
using api.Domain.Models;
using api.Domain.Repositories;
using api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> FirstOrDefaultAsync(string login, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }
    }
}