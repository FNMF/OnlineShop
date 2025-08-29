namespace API.Domain.ValueObjects
{
    public class RiderFeeMain
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public RiderInfo Rider { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
