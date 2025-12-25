namespace API.Domain.Events.MerchantCase
{
    public class ShopAdminRegisterEvent:IDomainEvent
    {
        public string Phone { get; }
        public DateTime OccurredOn { get; }
        public ShopAdminRegisterEvent(string phone)
        {
            Phone = phone;
            OccurredOn = DateTime.Now;
        }
    }
}
