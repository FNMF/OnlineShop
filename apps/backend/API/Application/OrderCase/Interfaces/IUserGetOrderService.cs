using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Application.OrderCase.Interfaces
{
    public interface IUserGetOrderService
    {
        Task<Result<OrderMain>> GetOrderByUuid(Guid uuid);
    }
}
