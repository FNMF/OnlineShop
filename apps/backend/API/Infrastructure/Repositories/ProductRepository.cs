using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly OnlineshopContext _context;
        public ProductRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Product> QueryProducts()
        {
            return _context.Products;
        }
        public async Task<bool> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
