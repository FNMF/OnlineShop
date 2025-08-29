using API.Api.Common.Models;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Application.OrderCase.Interfaces
{
    public interface IUserPutOrderService
    {
        Task<Result<OrderMain>> UserPutOrder(OrderWriteOptions opt);
    }
}
