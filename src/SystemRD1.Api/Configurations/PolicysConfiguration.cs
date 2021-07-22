using Microsoft.Extensions.DependencyInjection;

namespace SystemRD1.Api.Configurations
{
    public static class PolicysConfiguration
    {
        public static IServiceCollection AddPolicysConfiguration(this IServiceCollection services)
        {
            string[] claimsValue = new string[] { "Add", "Edit", "Delete" };

            services.AddAuthorization(options =>
            {
                options.AddPolicy("WriterCustomer", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("customer", claimsValue);
                });
            });

            return services;
        }
    }
}
