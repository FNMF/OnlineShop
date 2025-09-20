using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Aggregates.OrderAggregate.Interfaces
{
    public interface IOrderReadService
    {
        Task<Result<List<Order>>> UserGetAllOrdersByUuid(Guid uuid);
        Task<Result<List<Order>>> MerchantGetAllOrdersByUuid(Guid uuid);
        Task<Result<Order>> GetOrderByUuid(Guid uuid);
    }
}
