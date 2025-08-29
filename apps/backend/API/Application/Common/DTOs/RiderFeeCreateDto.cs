using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class RiderFeeCreateDto
    {
        public Guid UserUuid { get; set; }
        public Guid OrderUuid { get; set; }
        public Guid AddressUuid { get; set; }
        public Guid MerchantUuid { get; set; }
        public string UserAddress { get; set; }
        public string MerchantAddress { get; set; }
        public string ExpectedTime { get; set; }
        public OrderRiderService RiderService { get; set; }

        public RiderFeeCreateDto(Guid userUuid, Guid orderUuid, Guid addressUuid, Guid merchantUuid, string userAddress, string merchantAddress, string expectedTime, OrderRiderService riderService)
        {
            UserUuid = userUuid;
            OrderUuid = orderUuid;
            AddressUuid = addressUuid;
            MerchantUuid = merchantUuid;
            UserAddress = userAddress;
            MerchantAddress = merchantAddress;
            ExpectedTime = expectedTime;
            RiderService = riderService;
        }
    }
}
