namespace FeeMgmBackend.Entity;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public  string Username { get; set; }   
    public string Phone { get; set; }
    public string Designation { get; set; }
    public bool IsDeactivated { get; set; }
}