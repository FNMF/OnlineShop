using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Domain.Aggregates.OrderAggregate.Interfaces
{
    public interface IOrderCreateService
    {
        Task<Result<OrderMain>> CreateOrder(OrderMain orderMain);
    }
}
