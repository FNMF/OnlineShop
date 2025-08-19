using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class CartItemRepository: ICartItemRepository
    {
        private readonly OnlineshopContext _context;
        public CartItemRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Cartitem> QueryCartItems()
        {
            return _context.Cartitems;
        }
        public async Task<bool> AddCartItemAsync(Cartitem cartItem)
        {
            await _context.Cartitems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCartItemAsync(Cartitem cartItem)
        {
            _context.Cartitems.Update(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveCartItemAsync(Cartitem cartItem)
        {
            _context.Cartitems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
