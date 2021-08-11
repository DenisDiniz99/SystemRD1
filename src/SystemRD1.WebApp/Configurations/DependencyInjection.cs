using Microsoft.Extensions.DependencyInjection;
using SystemRD1.WebApp.Services.Authentication;

namespace SystemRD1.WebApp.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationServices, AuthenticationServices>();
            return services;
        }
    }
}
