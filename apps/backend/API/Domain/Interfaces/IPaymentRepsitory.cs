using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IPaymentRepsitory
    {
        IQueryable<Payment> QueryPayments();
        Task<bool> SaveChangesAsync();
        Task<bool> AddPaymentAsync(Payment payment);
        Task<bool> AddPaymenAsyncNoCommit(Payment payment);
        Task<bool> UpdatePaymentAsync(Payment payment);
        Task<bool> UpdatePaymentAsyncNoCommit(Payment payment);
        Task<bool> RemovePaymentAsync(Payment payment);
    }
}
