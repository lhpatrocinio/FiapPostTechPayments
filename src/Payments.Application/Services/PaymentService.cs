using Payments.Application.Repository;
using Payments.Application.Services.Interfaces;
using Payments.Domain.Entities;

namespace Payments.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task AddAsync(Payment Payment)
        {
            await _paymentRepository.AddAsync(Payment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            await _paymentRepository.DeleteAsync(payment);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment> GetByIdAsync(Guid id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Payment game)
        {
            await _paymentRepository.UpdateAsync(game);
        }
    }
}
