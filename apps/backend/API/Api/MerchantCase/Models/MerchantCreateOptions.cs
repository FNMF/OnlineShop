namespace API.Api.MerchantCase.Models
{
    public class MerchantCreateOptions
    {
        public string Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Detail { get; set; }
        public TimeOnly BusinessStart { get; set; }
        public TimeOnly BusinessEnd { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public decimal FreeDeliveryThreshold { get; set; }
    }
}
