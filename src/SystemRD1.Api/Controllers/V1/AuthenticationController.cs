using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Api.Extension;
using SystemRD1.Api.ViewModels;
using SystemRD1.Business.Contracts.Notifiers;

namespace SystemRD1.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/{controller}")]
    [Authorize]
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

        [AllowAnonymous]
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
                EmailConfirmed = true,                
            };

            
            var result = await _userManager.CreateAsync(user, registerUser.Passwaord);
            
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return ResponsePost(await GenerateJwt(registerUser.Email));
            }

            foreach(var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return ResponsePost(registerUser);
        }

        [AllowAnonymous]
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
                return ResponsePost(await GenerateJwt(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                NotifyError("Usuário bloqueado por tentativas inválidas");
                return ResponsePost(loginUser);
            }

            NotifyError("Usuário ou senha inválidos");
            return ResponsePost(loginUser);
        }


        [HttpPost("changePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordUserViewModel changePasswordUser)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            if(changePasswordUser.NewPassword != changePasswordUser.ConfirmNewPassword)
            {
                NotifyError("As senhas não correspondem.");
                return ResponsePost(changePasswordUser);
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordUser.CurrentPassword, changePasswordUser.NewPassword);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return ResponsePost(result);
            }

            foreach(var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return ResponsePost(changePasswordUser);
        }


        [HttpPost("profiles")]
        public async Task<ActionResult> AddUserProfile(ProfileViewModel profileViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            if(user == null)
            {
                NotifyError("Usuário inexistente.");
                return ResponsePost(user);
            }

            var claims = new List<Claim>();
            
            foreach(var c in profileViewModel.ClaimsViewModel)
            {
                if(c.ClaimType == string.Empty || c.ClaimValue == string.Empty)
                {
                    claims.Clear();
                    break;
                }
                Claim claim = new Claim(c.ClaimType, c.ClaimValue);
                claims.Add(claim);
            }

            if(claims.Count == 0)
            {
                NotifyError("Permissões não selecionadas ou incorretas.");
                return ResponsePost(claims);
            }

            var result = await _userManager.AddClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                NotifyError("Erro ao criar permissões.");
                return ResponsePost(result);
            }

            return ResponsePost(result);
        }


        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return ResponsePost();
        }


        private async Task<string> GenerateJwt(string email)
        {
            var identityClaims = await GetClaimsIdentity(email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string emailUser)
        {
            var identityUser = await _userManager.FindByEmailAsync(emailUser);
            var claims = await _userManager.GetClaimsAsync(identityUser);
            var roles = await _userManager.GetRolesAsync(identityUser);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, identityUser.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach(var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var claimIdentity = new ClaimsIdentity();
            claimIdentity.AddClaims(claims);

            return claimIdentity;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        
    }
}
