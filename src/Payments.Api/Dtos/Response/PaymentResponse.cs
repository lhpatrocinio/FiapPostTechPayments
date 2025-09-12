namespace Payments.Api.Dtos.Response
{
    public class PaymentResponse : BaseResponse
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
