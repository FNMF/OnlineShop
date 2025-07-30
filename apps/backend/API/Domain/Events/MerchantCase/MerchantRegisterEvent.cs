namespace API.Domain.Events.MerchantCase
{
    public class MerchantRegisterEvent:IDomainEvent
    {
        public string Phone { get; }
        public DateTime OccurrendOn { get; }
        public MerchantRegisterEvent(string phone)
        {
            Phone = phone;
            OccurrendOn = DateTime.Now;
        }
    }
}
