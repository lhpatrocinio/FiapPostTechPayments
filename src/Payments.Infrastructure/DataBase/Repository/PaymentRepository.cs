using Microsoft.EntityFrameworkCore;
using Payments.Application.Repository;
using Payments.Domain.Entities;
using Payments.Infrastructure.DataBase.EntityFramework.Context;
using System.Xml.Linq;

namespace Payments.Infrastructure.DataBase.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment entity)
        {
            await _context.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Payment entity)
        {
            _context.Set<Payment>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Set<Payment>().ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(Guid paymentId)
        {
            return await _context.Set<Payment>().Where(x => x.Id == paymentId).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment entity)
        {
            _context.Set<Payment>().Update(entity);
            await SaveChangesAsync();
        }
    }
}
