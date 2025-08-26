using API.Common.Helpers;

namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderMain
    {
        public Guid OrderUuid { get; private set; }
        public Guid OrderUseruuid { get; private set; }
        public Guid OrderPaymentuuid {  get; private set; }
        public decimal OrderTotal { get; private set; }
        public string OrderStatus { get; private set; }
        public string OrderSid { get; private set; }
        public DateTime OrderTime { get; private set; }
        public string OrderMa { get; private set; }
        public string OrderUa { get; private set; }
        public decimal OrderCost { get; private set; }
        public decimal OrderPackingcharge { get; private set; }
        public decimal OrderRidercost { get; private set; }
        public ICollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        // 这里可以是值对象，如配送方式，商品信息等
        public string OrderRiderservice { get; private set; }

        // 构造函数
        public OrderMain( Guid orderUseruuid, decimal orderTotal, string orderStatus, string orderSid,
                      DateTime orderTime, string orderMa, string orderUa, decimal orderCost, decimal orderPackingcharge,
                      decimal orderRidercost, string orderRiderservice, List<OrderItem> orderItems)
        {
            OrderUuid = UuidV7Helper.NewUuidV7();
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

            _orderItems = orderItems;
        }

        //内部构造函数，用于从数据库中加载数据时使用
        internal OrderMain(Guid orderUuid, Guid orderUseruuid, decimal orderTotal, string orderStatus, string orderSid,
                      DateTime orderTime, string orderMa, string orderUa, decimal orderCost, decimal orderPackingcharge,
                      decimal orderRidercost, string orderRiderservice, List<OrderItem> orderItems)
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
            _orderItems = orderItems ?? new List<OrderItem>();
        }

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
        public void AddOrderItem(Guid orderUuid, Guid productId, int quantity, decimal unitPrice,string name)
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
        public void RemoveOrderItem(Guid productUuid) 
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
        
        public void MarkAsPaid(Guid paymentUuid)
        {
            if(OrderStatus != Enums.OrderStatus.created.ToString())
            {
                throw new InvalidOperationException("订单必须是待支付状态才能支付");
            }
            OrderPaymentuuid = paymentUuid;
            OrderStatus = Enums.OrderStatus.paid.ToString();
            //可添加领域事件

        }
        public void MarkAsAccepted()
        {
            if (OrderStatus != Enums.OrderStatus.paid.ToString())
            {
                throw new InvalidOperationException("订单必须是已支付状态才能接受");
            }
            OrderStatus = Enums.OrderStatus.accepted.ToString();

        }
        public void MarkAsPrepared()
        {
            if (OrderStatus != Enums.OrderStatus.accepted.ToString())
            {
                throw new InvalidOperationException("订单必须是已接受状态才能准备完成"); 
            }
            OrderStatus = Enums.OrderStatus.prepared.ToString();
        }
        public void MarkAsShipped()
        {
            if (OrderStatus != Enums.OrderStatus.prepared.ToString())
            {
                throw new InvalidOperationException("订单必须是已准备完成状态才能配送");
            }
            OrderStatus = Enums.OrderStatus.shipped.ToString();
            
        }
        public void MarkAsComleted()
        {
            if (OrderStatus != Enums.OrderStatus.shipped.ToString())
            {
                throw new InvalidOperationException("订单必须是已配送状态才能完成");
            }
            OrderStatus = Enums.OrderStatus.completed.ToString();
        }
        public void MarkAsCanceled(string reason)        //用户取消
        {
            if (OrderStatus == Enums.OrderStatus.completed.ToString()|| OrderStatus == Enums.OrderStatus.rejected.ToString()|| OrderStatus == Enums.OrderStatus.completed.ToString())
            {
                throw new InvalidOperationException("订单必须是未完成状态才能取消");
            }
            OrderStatus =  Enums.OrderStatus.canceled.ToString();
        }
        public void MarkAsRejected(string reason)
        {
            if (OrderStatus != Enums.OrderStatus.paid.ToString())
            {
                throw new InvalidOperationException("订单必须是已支付状态才能拒绝");
            }
            OrderStatus = Enums.OrderStatus.rejected.ToString();
        }
        public void MarkAsExecption(string ex)
        {
            OrderStatus = Enums.OrderStatus.exception.ToString();
            //领域事件
        }

        private void AddDomainEvent(object @event)
        {
            // 将事件添加到领域事件队列，交给应用层或事件总线处理
        }
    }
}
