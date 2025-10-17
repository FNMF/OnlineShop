using API.Common.Helpers;
using API.Domain.Enums;

namespace API.Domain.Aggregates.OrderAggregates
{
    public class OrderMain
    {
        public Guid OrderUuid { get; private set; }
        public Guid OrderUseruuid { get; private set; }
        public Guid OrderPaymentuuid {  get; private set; }
        public decimal OrderTotal { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public string OrderSid { get; private set; }
        public DateTime OrderTime { get; private set; }
        public string OrderMa { get; private set; }
        public string OrderUa { get; private set; }
        public decimal OrderCost { get; private set; }
        public decimal OrderPackingcharge { get; private set; }
        public decimal OrderRidercost { get; private set; }
        public string? Note { get; private set; }
        public string ExpectedTime { get; private set; }
        public ICollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        // 这里可以是值对象，如配送方式，商品信息等
        public OrderRiderService OrderRiderservice { get; private set; }

        // 构造函数
        public OrderMain( Guid orderUseruuid, decimal orderTotal, OrderStatus orderStatus, string orderSid,
                      DateTime orderTime, string orderMa, string orderUa, decimal orderCost, decimal orderPackingcharge,
                      decimal orderRidercost, OrderRiderService orderRiderservice, string? note, string expectedTime, List<OrderItem> orderItems)
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
            Note = note;
            ExpectedTime = expectedTime;

            _orderItems = orderItems;
        }

        //内部构造函数，用于从数据库中加载数据时使用
        internal OrderMain(Guid orderUuid, Guid orderUseruuid, decimal orderTotal, OrderStatus orderStatus, string orderSid,
                      DateTime orderTime, string orderMa, string orderUa, decimal orderCost, decimal orderPackingcharge,
                      decimal orderRidercost, OrderRiderService orderRiderservice, string? note, string expectedTime, List<OrderItem> orderItems)
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
            Note = note;
            ExpectedTime = expectedTime;
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
        public void AddOrderItem(Guid orderUuid, Guid productUuid, Guid merchantUuid, int quantity, decimal unitPrice,string name, decimal packingFee)
        {
            // 业务规则：检查是否已存在该产品
            var existingItem = _orderItems.FirstOrDefault(i => i.ProductUuid == productUuid);
            if (existingItem != null)
            {
                existingItem.AddQuantity(quantity);
            }
            else
            {
                _orderItems.Add(new OrderItem(orderUuid,productUuid, merchantUuid, quantity, unitPrice,name, packingFee));
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
            if(OrderStatus != Enums.OrderStatus.created)
            {
                throw new InvalidOperationException("订单必须是待支付状态才能支付");
            }
            OrderPaymentuuid = paymentUuid;
            OrderStatus = Enums.OrderStatus.paid;

        }
        public void MarkAsAccepted()
        {
            if (OrderStatus != Enums.OrderStatus.paid)
            {
                throw new InvalidOperationException("订单必须是已支付状态才能接受");
            }
            OrderStatus = Enums.OrderStatus.accepted;

        }
        public void MarkAsPrepared()
        {
            if (OrderStatus != Enums.OrderStatus.accepted)
            {
                throw new InvalidOperationException("订单必须是已接受状态才能准备完成"); 
            }
            OrderStatus = Enums.OrderStatus.prepared;
        }
        public void MarkAsShipped()
        {
            if (OrderStatus != Enums.OrderStatus.prepared)
            {
                throw new InvalidOperationException("订单必须是已准备完成状态才能配送");
            }
            OrderStatus = Enums.OrderStatus.shipped;
            
        }
        public void MarkAsComleted()
        {
            if (OrderStatus != Enums.OrderStatus.shipped)
            {
                throw new InvalidOperationException("订单必须是已配送状态才能完成");
            }
            OrderStatus = Enums.OrderStatus.completed;
        }
        public void MarkAsCanceled(string reason)        //用户取消
        {
            if (OrderStatus == Enums.OrderStatus.completed || OrderStatus == Enums.OrderStatus.rejected || OrderStatus == Enums.OrderStatus.completed)
            {
                throw new InvalidOperationException("订单必须是未完成状态才能取消");
            }
            OrderStatus =  Enums.OrderStatus.canceled;
        }
        public void MarkAsRejected(string reason)
        {
            if (OrderStatus != Enums.OrderStatus.paid)
            {
                throw new InvalidOperationException("订单必须是已支付状态才能拒绝");
            }
            OrderStatus = Enums.OrderStatus.rejected;
        }
        public void MarkAsExecption(string ex)
        {
            OrderStatus = Enums.OrderStatus.exception;
            //领域事件
        }

        private void AddDomainEvent(object @event)
        {
            // 将事件添加到领域事件队列，交给应用层或事件总线处理
        }
    }
}
