using Payments.Domain.Entities;

namespace Payments.Api.Dtos.Requests
{
    public class PaymentRequest
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}

