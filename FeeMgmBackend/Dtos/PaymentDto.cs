using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public string Note { get; set; }
    }
}
