using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        IQueryable<Cartitem> QueryCartItems();
        Task<bool> AddCartItemAsync(Cartitem cartItem);
        Task<bool> UpdateCartItemAsync(Cartitem cartItem);
        Task<bool> RemoveCartItemAsync(Cartitem cartItem);
    }
}
