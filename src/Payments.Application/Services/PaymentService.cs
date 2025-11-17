using Payments.Application.Producer;
using Payments.Application.Repository;
using Payments.Application.Services.Interfaces;
using Payments.Domain.Entities;

namespace Payments.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentCompletedProducer _paymentCompletedProducer;
        public PaymentService(IPaymentRepository paymentRepository, IPaymentCompletedProducer paymentCompletedProducer)
        {
            _paymentRepository = paymentRepository;
            _paymentCompletedProducer = paymentCompletedProducer;
        }

        public async Task AddAsync(Payment Payment)
        {
            await _paymentRepository.AddAsync(Payment);
            _paymentCompletedProducer.PublishUserActiveEvent(new PaymentCompletedProducer.PaymentCompleted()
            {
                GameId = Payment.GameId,
                UserId = Payment.UserId
            });
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
