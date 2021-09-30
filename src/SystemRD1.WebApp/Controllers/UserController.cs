using Microsoft.AspNetCore.Mvc;
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

            var response = await _authenticationServices.RegisterService(model);

            if (ResponseModelStateErrors(response.ResponseResult)) return View(model);

            await _authenticationServices.ConfirmLogin(response);

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

            
            var response = await _authenticationServices.LoginService(model);

            if (ResponseModelStateErrors(response.ResponseResult)) return View(model);

            await _authenticationServices.ConfirmLogin(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationServices.LogoutService();
            return RedirectToAction("Index", "Home");
        }
    }
}
