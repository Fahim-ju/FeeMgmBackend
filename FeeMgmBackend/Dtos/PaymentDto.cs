using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public string Note { get; set; }
    }
}
