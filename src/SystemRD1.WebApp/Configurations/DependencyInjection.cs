using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SystemRD1.WebApp.Extensions.User;
using SystemRD1.WebApp.Services.Authentication;

namespace SystemRD1.WebApp.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationServices, AuthenticationServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            return services;
        }
    }
}
