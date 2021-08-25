using System.Threading.Tasks;
using SystemRD1.WebApp.Models.User;

namespace SystemRD1.WebApp.Services.Authentication
{
    public interface IAuthenticationServices
    {
        Task<ResponseUserViewModel> RegisterService(RegisterUserViewModel model);
        Task<ResponseUserViewModel> LoginService(LoginUserViewModel model);
        Task LogoutService();
        Task ConfirmLogin(ResponseUserViewModel responseUserViewModel);
        bool ExpiredToken();
    }
}
