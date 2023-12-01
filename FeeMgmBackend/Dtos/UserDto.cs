using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Dtos
{
    public class UserDto : User
    {
        public decimal TotalFine { get; set; }
        public decimal Paid { get; set; }
        public decimal Due { get; set; }
    }
}
