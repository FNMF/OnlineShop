using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class PaymentRepository: IPaymentRepsitory
    {
        private readonly OnlineshopContext _context;
        public PaymentRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Payment> QueryPayments()
        {
            return _context.Payments;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return await SaveChangesAsync();
        }
        public async Task<bool> AddPaymenAsyncNoCommit(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return true;
        }
        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            return await SaveChangesAsync();
        }
        public async Task<bool> UpdatePaymentAsyncNoCommit(Payment payment)
        {
            _context.Payments.Update(payment);
            return true;
        }
        public async Task<bool> RemovePaymentAsync(Payment payment)
        {
            _context.Payments.Remove(payment);
            return await SaveChangesAsync();
        }
    }
}
