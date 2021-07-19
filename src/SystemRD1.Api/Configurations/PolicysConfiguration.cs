using Microsoft.Extensions.DependencyInjection;

namespace SystemRD1.Api.Configurations
{
    public static class PolicysConfiguration
    {
        public static IServiceCollection AddPolicysConfiguration(this IServiceCollection services)
        {
            string[] claimsValue = new string[] { "Add", "Edit", "Delete" };

            services.AddAuthorization(option =>
            {
                option.AddPolicy("WriterCustomer", policy => policy.RequireClaim("Customer", "Add", "Edit", "Delete"));
            });

            return services;
        }
    }
}
