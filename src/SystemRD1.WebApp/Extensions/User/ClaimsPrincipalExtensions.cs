using System;
using System.Security.Claims;

namespace SystemRD1.WebApp.Extensions.User
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if(claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            if(claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal claimsPrincipal)
        {
            if(claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}