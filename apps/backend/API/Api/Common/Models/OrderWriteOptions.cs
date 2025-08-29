using API.Domain.Enums;

namespace API.Api.Common.Models
{
    public class OrderWriteOptions
    {
        public Guid CartUuid { get; set; }
        public Guid AddressUuid { get; set; }
        public Guid? CouponUuid { get; set; }
        public OrderRiderService RiderService { get; set; }
        public string ExpectedTime { get; set; }
        public OrderChannel OrderChannel { get; set; }
        public string? Note { get; set; }

    }
}
