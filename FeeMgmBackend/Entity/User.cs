namespace FeeMgmBackend.Entity;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public bool IsDeactivated { get; set; }
    public int TotalFine { get; set; }
    public int Paid { get; set; }
    public int Due { get; set; }
}