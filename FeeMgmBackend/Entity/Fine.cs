namespace FeeMgmBackend.Entity;

public class Fine
{
    public Guid Id { get; set; }

    public Guid LawId { get; set; }
    public Law Law { get; set; }
    public decimal amount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;

    public string Note { get; set; }

    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public DateTime FineDate { get; set; }
}