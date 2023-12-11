using FeeMgmBackend.Models;

namespace FeeMgmBackend.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel registrationModel, string role);
        Task<(int, string)> Login(LoginModel loginModel);
    }
}
