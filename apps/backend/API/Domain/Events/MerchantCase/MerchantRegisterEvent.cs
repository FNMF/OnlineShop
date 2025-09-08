namespace API.Domain.Events.MerchantCase
{
    public class MerchantRegisterEvent:IDomainEvent
    {
        public string Phone { get; }
        public DateTime OccurredOn { get; }
        public MerchantRegisterEvent(string phone)
        {
            Phone = phone;
            OccurredOn = DateTime.Now;
        }
    }
}
