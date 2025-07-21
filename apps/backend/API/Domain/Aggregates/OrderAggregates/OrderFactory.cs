namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderFactory
    {
        public Order CreateOrder(byte[] orderUuid, byte[] orderUseruuid, decimal orderTotal, string orderStatus, string orderSid,
                             DateTime orderTime, string orderMa, string orderUa, decimal orderCost, decimal orderPackingcharge,
                             decimal orderRidercost, string orderRiderservice)
        {



            // 可以在这里加入更复杂的创建规则，例如验证传入参数等
            return Order.CreateOrder(orderUuid, orderUseruuid, orderTotal, orderStatus, orderSid,
                                     orderTime, orderMa, orderUa, orderCost, orderPackingcharge,
                                     orderRidercost, orderRiderservice);
        }
    }
}
