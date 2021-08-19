using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Api.Extension;
using SystemRD1.Api.Services.Email;
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
        private readonly IEmailSender _emailSender;


        public AuthenticationController(INotifier notifier, 
                                        UserManager<IdentityUser> userManager, 
                                        SignInManager<IdentityUser> signInManager,
                                        IOptions<AppSettings> appSettings,
                                        IEmailSender emailSender) : base (notifier) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var user = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,                
            };

            
            var result = await _userManager.CreateAsync(user, model.Passwaord);
            
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return ResponsePost(await GenerateJwt(model.Email));
            }

            foreach(var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return ResponsePost(model);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                return ResponsePost(await GenerateJwt(model.Email));
            }

            if (result.IsLockedOut)
            {
                NotifyError("Usuário bloqueado por tentativas inválidas");
                return ResponsePost(model);
            }

            NotifyError("Usuário ou senha inválidos");
            return ResponsePost(model);
        }


        [HttpPost("changePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword,  model.NewPassword);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return ResponsePost(result);
            }

            foreach(var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return ResponsePost(model);
        }


        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                NotifyError("E-mail incorreto.");
                return ResponsePost();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl = $"{Request.Scheme}://localhost:44328/api/v1/authentication/resetPassword?userId={user.Id}&token={token}";

            await _emailSender.SendEmailAsync(user.Email, "Recuperação de Senha", "Recupere por - " + callBackUrl);

            return ResponsePost(callBackUrl);
        }


        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<ActionResult> ResetPassword(Guid userId, string token, ResetPasswordUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user == null)
            {
                NotifyError("Usuário incorreto.");
                return ResponsePost();
            }

            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                return ResponsePost(result);
            }

            foreach(var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return ResponsePost();
        }

        [HttpPost("profiles")]
        public async Task<ActionResult> AddUserProfile(ProfileViewModel model)
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
            
            foreach(var c in model.ClaimsViewModel)
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







        //Generate Json Web Token
        private async Task<ResponseResultLogin> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var identityClaims = await GetClaimsIdentity(email);
            var encodedToken = GetEncodedToken(identityClaims);

            return GenerateResponseUser(encodedToken, user, claims);
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

        //novo
        private string GetEncodedToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

           return tokenHandler.WriteToken(token);
        }

        //novo
        private ResponseResultLogin GenerateResponseUser(string encodedToken, IdentityUser identityUser, IEnumerable<Claim> claims)
        {
            return new ResponseResultLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                ResponseUser = new ResponseUser
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email,
                    Claims = claims.Select(c => new ResponseUserClaims
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

       
    }
}
