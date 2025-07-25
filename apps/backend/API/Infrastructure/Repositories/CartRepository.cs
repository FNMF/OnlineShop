using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class CartRepository:ICartRepository
    {
        private readonly OnlineshopContext _context;

        public CartRepository(OnlineshopContext context)
        {
            _context = context;
        }

        public IQueryable<Cart> QueryCarts()
        {
            return _context.Carts;
        }

        public async Task<bool> AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCartAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
