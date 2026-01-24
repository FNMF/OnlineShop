namespace API.Application.MerchantCase.DTOs
{
    public class AdminMerchantResult
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Detail { get; set; }
        public TimeOnly BusinessStart { get; set; }
        public TimeOnly BusinessEnd { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public decimal? FreeDeliveryThreshold { get; set; }
        public bool? IsClosed { get; set; }
        public bool IsAudited { get; set; }
    }
}
