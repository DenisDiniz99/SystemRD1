using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemRD1.Api.Services.Email;

namespace SystemRD1.Api.Configurations
{
    public static class EmailConfiguration
    {
        public static IServiceCollection AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthMessageSenderOptions>(options => 
            {
                options.SendGridKey = configuration["ExternalProvider:SendGrid:ApiKey"];
                options.SendGridUser = configuration["ExternalProvider:SendGrid:SenderEmail"];
            });

            return services;
        }
    }
}
