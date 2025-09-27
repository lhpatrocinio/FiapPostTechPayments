using Payments.Domain.Entities;

namespace Payments.Application.Repository
{
    public interface IPaymentRepository 
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(Guid paymentId);
        Task AddAsync(Payment entity);
        Task UpdateAsync(Payment entity);
        Task DeleteAsync(Payment entity);
        Task SaveChangesAsync();
    }
}
