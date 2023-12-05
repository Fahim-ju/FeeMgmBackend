using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Dtos
{
    public class FineDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string LawName { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;

        public string Note { get; set; }
    }
}
