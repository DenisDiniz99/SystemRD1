using System.Net.Http;
using System.Threading.Tasks;
using SystemRD1.WebApp.Models.User;

namespace SystemRD1.WebApp.Services.Authentication
{
    public class AuthenticationServices : Services, IAuthenticationServices
    {
        private readonly HttpClient _httpClient;

        public AuthenticationServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseUserViewModel> LoginUser(LoginUserViewModel model)
        {
            var loginContent = GetContent(model);

            var response = await _httpClient.PostAsync("/api/v1/authentication/login", loginContent);

            return await DeserializeObjectResponse<ResponseUserViewModel>(response);
        }

        public async Task<ResponseUserViewModel> RegisterUser(RegisterUserViewModel model)
        {
            var registerContent = GetContent(model);

            var response = await _httpClient.PostAsync("/api/v1/authentication/register", registerContent);

            return await DeserializeObjectResponse<ResponseUserViewModel>(response);
        }
    }
}
