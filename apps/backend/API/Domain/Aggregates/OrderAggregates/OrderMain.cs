using API.Domain.Events;

namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderMain
    {
        public byte[] OrderUuid { get; set; }
        public byte[] OrderUseruuid { get;set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public string OrderSid { get; set; }
        public DateTime OrderTime { get; set; }
        public string OrderMa { get; set; }
        public string OrderUa { get; set; }
        public decimal OrderCost { get; set; }
        public decimal OrderPackingcharge { get; set; }
        public decimal OrderRidercost { get; set; }
        public ICollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        // 这里可以是值对象，如配送方式，商品信息等
        public string OrderRiderservice { get; set; }

        // 构造函数
        /*public OrderMain(byte[] orderUuid, byte[] orderUseruuid, decimal orderTotal, string orderStatus, string orderSid,
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
        }*/

        // 创建订单时的业务逻辑
        /*public static OrderMain CreateOrder(byte[] orderUuid, byte[] orderUseruuid, decimal orderTotal, string orderStatus,
                                        string orderSid, DateTime orderTime, string orderMa, string orderUa, decimal orderCost,
                                        decimal orderPackingcharge, decimal orderRidercost, string orderRiderservice)
        {
            // 这里可以添加一些业务规则，比如订单状态是否合法
            if (orderTotal <= 0) throw new ArgumentException("Order total must be greater than zero.");

            var order = new OrderMain(orderUuid, orderUseruuid, orderTotal, orderStatus, orderSid,
                                  orderTime, orderMa, orderUa, orderCost, orderPackingcharge,
                                  orderRidercost, orderRiderservice);

            // 可以触发领域事件（例如订单创建事件）
            //order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }*/

        // 改变订单状态的业务逻辑
        public void AddOrderItem(byte[] orderUuid, byte[] productId, int quantity, decimal unitPrice,string name)
        {
            // 业务规则：检查是否已存在该产品
            var existingItem = _orderItems.FirstOrDefault(i => i.ProductUuid == productId);
            if (existingItem != null)
            {
                existingItem.AddQuantity(quantity);
            }
            else
            {
                _orderItems.Add(new OrderItem(orderUuid,productId, quantity, unitPrice,name));
            }
            RecalculateTotal();
        }
        public void RemoveOrderItem(byte[] productUuid) 
        {
            var item = _orderItems.FirstOrDefault(i =>i.ProductUuid == productUuid);
            if (item == null)
            {
                throw new InvalidOperationException("商品不存在");
            }
            _orderItems.Remove(item);
            RecalculateTotal();
        }

        private void RecalculateTotal() => OrderTotal = _orderItems.Sum(i => i.SubTotal);
        
        public void MarkAsPaid()
        {

        }

    }
}
