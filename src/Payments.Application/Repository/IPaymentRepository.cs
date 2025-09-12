using Payments.Domain.Entities;

namespace Payments.Application.Repository
{
    public interface IPaymentRepository 
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(Guid paymentId);
        Task AddAsync(Payment entity);
        void Update(Payment entity);
        void Delete(Payment entity);
        Task SaveChangesAsync();
    }
}
