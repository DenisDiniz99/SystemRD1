using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemRD1.WebApp.Models.User;
using SystemRD1.WebApp.Services.Authentication;

namespace SystemRD1.WebApp.Controllers
{
    public class UserController : WebController
    {
        private readonly IAuthenticationServices _authenticationServices;

        public UserController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Register(RegisterUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return View(model);

            var response = await _authenticationServices.RegisterUser(model);

            await ConfirmLogin(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("entrar")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("entrar")]
        public async Task<IActionResult> Login(LoginUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return View(model);

            
            var response = await _authenticationServices.LoginUser(model);

            if (ResponseModelStateErrors(response.ResponseResult)) return View(model);

            await ConfirmLogin(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }


        private async Task ConfirmLogin(ResponseUserViewModel model)
        {
            var token = GetFormattedToken(model.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", model.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        private JwtSecurityToken GetFormattedToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        }
    }
}
