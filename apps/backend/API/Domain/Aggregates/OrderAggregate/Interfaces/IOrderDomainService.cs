using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Domain.Aggregates.OrderAggregate.Interfaces
{
    public interface IOrderDomainService
    {
        Task<Result<OrderMain>> AddItems(OrderMain orderMain, CartMain cartMain);
    }
}
