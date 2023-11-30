namespace FeeMgmBackend.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }
        public DateTime PaidAt { get; set; }   = DateTime.UtcNow;
        public string Note { get; set; }
    }
}
