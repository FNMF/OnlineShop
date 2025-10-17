namespace API.Domain.Interfaces
{
    public interface IUnitOfRepository
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
