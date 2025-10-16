using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Domain.Aggregates.OrderAggregate.Interfaces
{
    public interface IOrderUpdateService
    {
        Task<Result<OrderMain>> UpdateOrder(OrderMain orderMain);
        Result<OrderMain> UpdateOrderNoCommit(OrderMain orderMain);
    }
}
