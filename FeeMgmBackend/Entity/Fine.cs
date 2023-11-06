namespace FeeMgmBackend.Entity;

public class Fine
{
    public Guid Id { get; set; }
        
    // reference to Law
    public Guid LawId { get; set; }
    public Law Law { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
    public bool IsDeleted { get; set; }
        
    public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;
        
    public string Note { get; set; }
        
    public Guid UserId { get; set; }
    public User User { get; set; }
}