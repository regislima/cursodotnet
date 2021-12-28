using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain.Models;
using api.Domain.Repositories;
using api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.Include(cat => cat.Products).FirstOrDefaultAsync(cat => cat.Id == id);
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task<IEnumerable<Category>> FindByNameAsync(string name)
        {
            return await _context.Categories.Where(c => c.Name.Contains(name)).ToListAsync();
        }
    }
}