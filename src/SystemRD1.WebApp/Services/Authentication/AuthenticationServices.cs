using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemRD1.WebApp.Extensions;
using SystemRD1.WebApp.Extensions.User;
using SystemRD1.WebApp.Models.User;

namespace SystemRD1.WebApp.Services.Authentication
{
    public class AuthenticationServices : Services, IAuthenticationServices
    {
        const string BASE_URL = "https://localhost:44328";
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAspNetUser _aspNetUser;

        public AuthenticationServices(HttpClient httpClient, IAuthenticationService authenticationService, IAspNetUser aspNetUser)
        {
            httpClient.BaseAddress = new Uri(BASE_URL);

            _httpClient = httpClient;

            _authenticationService = authenticationService;

            _aspNetUser = aspNetUser;
        }

        public async Task<ResponseUserViewModel> RegisterService(RegisterUserViewModel model)
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

        public async Task<ResponseUserViewModel> LoginService(LoginUserViewModel model)
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

        public async Task LogoutService()
        {
            await _authenticationService.SignOutAsync(_aspNetUser.GetHttpContext(), 
                                                      CookieAuthenticationDefaults.AuthenticationScheme,
                                                      null);
        }


        public async Task ConfirmLogin(ResponseUserViewModel model)
        {
            var token = GetFormattedToken(model.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", model.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            
            await _authenticationService.SignInAsync(_aspNetUser.GetHttpContext(), 
                                                     CookieAuthenticationDefaults.AuthenticationScheme, 
                                                     new ClaimsPrincipal(claimsIdentity), 
                                                     authProperties);
        }

        public bool ExpiredToken()
        {
            var token = _aspNetUser.GetUserToken();
            if (token == null) return false;

            var tokenFormatted = GetFormattedToken(token);
            return tokenFormatted.ValidTo.ToLocalTime() < DateTime.Now;
        }


        private JwtSecurityToken GetFormattedToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        }
    }
}