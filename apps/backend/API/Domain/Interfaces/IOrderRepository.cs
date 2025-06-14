using API.Domain.Entities.Models;

namespace API.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByUserUuidAsync(Guid useruuid);
        // Task<Order> CreateOrderWithUuidAsync(Guid useruuid)
    }
}
