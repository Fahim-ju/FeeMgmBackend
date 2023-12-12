namespace FeeMgmBackend.Dtos
{
    public class AuthUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public Boolean EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; } 
        public Boolean PhoneNumberConfirmed { get; set;}
        public decimal AccessFailedCount { get; set; }  
      
    }
}
