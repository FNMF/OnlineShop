using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Application.OrderCase.Interfaces
{
    public interface IMerchantGetAllOrdersService
    {
        Task<Result<List<OrderMain>>> GetAllOrders();
    }
}
