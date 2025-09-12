using Payments.Application.Repository;
using Payments.Domain.Entities;
using Payments.Infrastructure.DataBase.EntityFramework.Context;

namespace Payments.Infrastructure.DataBase.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Payment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Payment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetByIdAsync(Guid paymentId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Payment entity)
        {
            throw new NotImplementedException();
        }
    }
}
