
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemRD1.Api.ViewModels;
using SystemRD1.Business.Notifications;

namespace SystemRD1.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/{controller}")]
    public class AuthenticationController : ApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthenticationController(Notifier notifier, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : base (notifier) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var user = new IdentityUser()
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Passwaord);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return ResponsePost(result);
            }

            foreach(var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return ResponsePost(registerUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Passwaord, false, true);

            if (result.Succeeded)
            {
                return ResponsePost(loginUser);
            }

            if (result.IsLockedOut)
            {
                NotifyError("Usuário bloqueado por tentativas inválidas");
                return ResponsePost(loginUser);
            }

            NotifyError("Usuário ou senha inválidos");
            return ResponsePost(loginUser);
        }
    }
}
