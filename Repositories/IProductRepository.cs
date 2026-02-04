using ECommerceApp.Models;

namespace ECommerceApp.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetTopAsync(int count);
    }
}