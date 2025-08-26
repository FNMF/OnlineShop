using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly OnlineshopContext _context;
        public OrderRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Order> QueryOrders()
        {
            return _context.Orders;
        }
        public async Task<bool> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
