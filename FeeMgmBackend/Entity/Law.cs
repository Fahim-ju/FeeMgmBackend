namespace FeeMgmBackend.Entity;

public class Law
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public decimal Amount { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;
}