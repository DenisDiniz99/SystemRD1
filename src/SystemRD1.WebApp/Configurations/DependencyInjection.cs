using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SystemRD1.WebApp.Extensions.User;
using SystemRD1.WebApp.Services.Authentication;
using SystemRD1.WebApp.Services.Customer;
using SystemRD1.WebApp.Services.Handler;

namespace SystemRD1.WebApp.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationServices, AuthenticationServices>();

            services.AddHttpClient<ICustomerServices, CustomerServices>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }
    }
}
