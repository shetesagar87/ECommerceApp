using ECommerceApp.DAL;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet.Include(p => p.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetTopAsync(int count)
        {
            return await _dbSet.OrderByDescending(p => p.ProductId).Take(count).ToListAsync();
        }
    }
}