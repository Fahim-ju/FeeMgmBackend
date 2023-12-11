namespace FeeMgmBackend.Dtos;

public class LawDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; } = 0;
}