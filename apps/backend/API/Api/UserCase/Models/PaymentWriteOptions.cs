using API.Domain.Enums;

namespace API.Api.UserCase.Models
{
    public class PaymentWriteOptions
    {
        public OrderChannel OrderChannel { get; set; }
        public Guid OrderUuid { get; set; }
        public string Description { get; set; }
    }
}
