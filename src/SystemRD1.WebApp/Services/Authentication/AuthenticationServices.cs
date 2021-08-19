using System;
using System.Net.Http;
using System.Threading.Tasks;
using SystemRD1.WebApp.Extensions;
using SystemRD1.WebApp.Models.User;

namespace SystemRD1.WebApp.Services.Authentication
{
    public class AuthenticationServices : Services, IAuthenticationServices
    {
        const string BASE_URL = "https://localhost:44328";
        private readonly HttpClient _httpClient;

        public AuthenticationServices(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(BASE_URL);

            _httpClient = httpClient;
        }

        public async Task<ResponseUserViewModel> LoginUser(LoginUserViewModel model)
        {
            var loginContent = GetContent(model);
           
            var response = await _httpClient.PostAsync("/api/v1/authentication/login", loginContent);

            if (!HandleErrors(response))
            {
                return new ResponseUserViewModel
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<ResponseUserViewModel>(response);

        }

        public async Task<ResponseUserViewModel> RegisterUser(RegisterUserViewModel model)
        {
            var registerContent = GetContent(model);

            var response = await _httpClient.PostAsync("/api/v1/authentication/register", registerContent);

            if (!HandleErrors(response))
            {
                return new ResponseUserViewModel
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<ResponseUserViewModel>(response);
        }
    }
}
