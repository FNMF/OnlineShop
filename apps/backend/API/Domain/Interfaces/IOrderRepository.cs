using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> QueryOrders();
        Task<bool> AddOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> RemoveOrderAsync(Order order);
    }
}
