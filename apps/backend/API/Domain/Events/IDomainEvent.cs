namespace API.Domain.Events
{
    public interface IDomainEvent
    {
        DateTime OccurrendOn { get; }
    }
}
