namespace API.Application.Common.DTOs
{
    public class OrderMainCreateDto
    {
        public Guid OrderUuid { get; }
        public Guid OrderUseruuid { get; }
        public decimal OrderTotal { get; }
        public string OrderStatus { get; }
        public string OrderSid { get; }
        public DateTime OrderTime { get; }
        public string OrderMa { get; }
        public string OrderUa { get; }
        public decimal OrderCost {  get; }
        public decimal OrderPackingcharge {  get; }
        public decimal OrderRidercost { get; }
        public string OrderRiderservice {  get; }
        public OrderMainCreateDto(Guid orderUuid, Guid orderUseruuid, decimal orderTotal, string orderStatus, string orderSid,
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
        }
    }
}
