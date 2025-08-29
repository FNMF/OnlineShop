using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Domain.Aggregates.OrderAggregate.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        public OrderDomainService() { }
        public Task<Result<OrderMain>> AddItems(OrderMain orderMain, CartMain cartMain)
        {
            try
            {
                foreach (var item in cartMain.Items)
                {
                    orderMain.AddOrderItem(
                        orderMain.OrderUuid,
                        item.ProductUuid,
                        item.Quantity,
                        item.Price,
                        item.ProductName,
                        item.PackingFee
                        );
                }
                return Task.FromResult(Result<OrderMain>.Success(orderMain));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<OrderMain>.Fail(ResultCode.ServerError, ex.Message));
            }


        }
    }
}
