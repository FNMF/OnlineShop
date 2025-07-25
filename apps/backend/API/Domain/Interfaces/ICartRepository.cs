using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface ICartRepository
    {
        IQueryable<Cart> QueryCarts();
        Task<bool> AddCartAsync(Cart cart);
        Task<bool> UpdateCartAsync(Cart cart);
        Task<bool> RemoveCartAsync(Cart cart);
    }
}
