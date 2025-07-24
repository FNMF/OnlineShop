namespace API.Domain.Events.MerchantCase
{
    public class MerchantLoginEvent:IDomainEvent
    {
        public int MerchantAdminAccount { get;}
        public DateTime OccurrendOn { get;}
        public MerchantLoginEvent(int merchantAdminAccount)
        {
            MerchantAdminAccount = merchantAdminAccount;
            OccurrendOn = DateTime.Now;
        }
    }
}
