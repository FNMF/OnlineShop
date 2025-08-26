using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Application.OrderCase.Interfaces
{
    public interface IUserGetAllOrdersService
    {
        Task<Result<List<OrderMain>>> GetAllOrders();
    }
}
