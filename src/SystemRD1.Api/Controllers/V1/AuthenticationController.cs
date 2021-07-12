using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Api.Extension;
using SystemRD1.Api.ViewModels;
using SystemRD1.Business.Contracts.Notifiers;

namespace SystemRD1.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/{controller}")]
    public class AuthenticationController : ApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;


        public AuthenticationController(INotifier notifier, 
                                        UserManager<IdentityUser> userManager, 
                                        SignInManager<IdentityUser> signInManager,
                                        IOptions<AppSettings> appSettings) : base (notifier) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
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
                return ResponsePost(GenerateJwt());
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
                return ResponsePost(GenerateJwt());
            }

            if (result.IsLockedOut)
            {
                NotifyError("Usuário bloqueado por tentativas inválidas");
                return ResponsePost(loginUser);
            }

            NotifyError("Usuário ou senha inválidos");
            return ResponsePost(loginUser);
        }

        private string GenerateJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
