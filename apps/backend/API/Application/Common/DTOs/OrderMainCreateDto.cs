using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class OrderMainCreateDto
    {
        public Guid OrderUuid { get; }
        public Guid UserUuid { get; }
        public decimal OrderTotal { get; }
        public OrderStatus OrderStatus { get; }
        public string OrderShortId { get; }
        public DateTime CreatedAt { get; }
        public string MerchantAddress { get; }
        public string UserAddress { get; }
        public decimal OrderListCost {  get; }
        public decimal PackingCost {  get; }
        public decimal RiderCost { get; }
        public OrderRiderService RiderService {  get; }
        public string ExpectedTime {  get; }
        public string? Note {  get; }
       
        public OrderMainCreateDto(Guid orderUuid, Guid userUuid, decimal orderTotal, OrderStatus orderStatus, string orderShortId, DateTime createdAt, string merchantAddress, string userAddress, decimal orderListCost, decimal packingCost, decimal riderCost, OrderRiderService riderService, string expectedTime, string? note)
        {
            OrderUuid = orderUuid;
            UserUuid = userUuid;
            OrderTotal = orderTotal;
            OrderStatus = orderStatus;
            OrderShortId = orderShortId;
            CreatedAt = createdAt;
            MerchantAddress = merchantAddress;
            UserAddress = userAddress;
            OrderListCost = orderListCost;
            PackingCost = packingCost;
            RiderCost = riderCost;
            RiderService = riderService;
            ExpectedTime = expectedTime;
            Note = note;
        }
    }
}
