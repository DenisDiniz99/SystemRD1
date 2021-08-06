using System.Threading.Tasks;
using SystemRD1.WebApp.Models.User;

namespace SystemRD1.WebApp.Services.Authentication
{
    public interface IAuthenticationServices
    {
        Task<ResponseUserViewModel> RegisterUser(RegisterUserViewModel model);
        Task<ResponseUserViewModel> LoginUser(LoginUserViewModel model);
    }
}
