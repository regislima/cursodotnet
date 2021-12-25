using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain.Models;
using api.Domain.Repositories;
using api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.Include(product => product.Category).ToListAsync();
        }

        public void Remove(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public void update(Product entity)
        {
            _context.Products.Update(entity);
        }

        public async Task<IEnumerable<Product>> FindByNameAsync(string name)
        {
            return await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }
    }
}