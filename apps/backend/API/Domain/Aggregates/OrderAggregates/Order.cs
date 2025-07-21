using API.Domain.Events;

namespace API.Domain.Aggregates.OrderAggregates
{
    public class Order
    {
        public byte[] OrderUuid { get; private set; }
        public byte[] OrderUseruuid { get; private set; }
        public decimal OrderTotal { get; private set; }
        public string OrderStatus { get; private set; }
        public string OrderSid { get; private set; }
        public DateTime OrderTime { get; private set; }
        public string OrderMa { get; private set; }
        public string OrderUa { get; private set; }
        public decimal OrderCost { get; private set; }
        public decimal OrderPackingcharge { get; private set; }
        public decimal OrderRidercost { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; }

        // 这里可以是值对象，如配送方式，商品信息等
        public string OrderRiderservice { get; private set; }

        // 构造函数
        private Order(byte[] orderUuid, byte[] orderUseruuid, decimal orderTotal, string orderStatus, string orderSid,
                      DateTime orderTime, string orderMa, string orderUa, decimal orderCost, decimal orderPackingcharge,
                      decimal orderRidercost, string orderRiderservice)
        {
            OrderUuid = orderUuid;
            OrderUseruuid = orderUseruuid;
            OrderTotal = orderTotal;
            OrderStatus = orderStatus;
            OrderSid = orderSid;
            OrderTime = orderTime;
            OrderMa = orderMa;
            OrderUa = orderUa;
            OrderCost = orderCost;
            OrderPackingcharge = orderPackingcharge;
            OrderRidercost = orderRidercost;
            OrderRiderservice = orderRiderservice;

            OrderItems = new List<OrderItem>();
        }

        // 创建订单时的业务逻辑
        public static Order CreateOrder(byte[] orderUuid, byte[] orderUseruuid, decimal orderTotal, string orderStatus,
                                        string orderSid, DateTime orderTime, string orderMa, string orderUa, decimal orderCost,
                                        decimal orderPackingcharge, decimal orderRidercost, string orderRiderservice)
        {
            // 这里可以添加一些业务规则，比如订单状态是否合法
            if (orderTotal <= 0) throw new ArgumentException("Order total must be greater than zero.");

            var order = new Order(orderUuid, orderUseruuid, orderTotal, orderStatus, orderSid,
                                  orderTime, orderMa, orderUa, orderCost, orderPackingcharge,
                                  orderRidercost, orderRiderservice);

            // 可以触发领域事件（例如订单创建事件）
            //order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        // 改变订单状态的业务逻辑
        public void ChangeOrderStatus(string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus)) throw new ArgumentException("Order status cannot be empty.");
            OrderStatus = newStatus;

            // 订单状态更改的领域事件
            //AddDomainEvent(new OrderStatusChangedEvent(this));
        }

        // 添加领域事件
        private void AddDomainEvent(IDomainEvent domainEvent)
        {
            // 可以使用事件管理器或事件总线将事件发布出去
        }
    }
}
