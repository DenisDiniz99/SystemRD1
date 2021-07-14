using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemRD1.Api.Configurations
{
    public static class PolicysConfiguration
    {
        public static IServiceCollection AddPolicysConfiguration(this IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Write", policy => policy.RequireClaim("Write"));
                option.AddPolicy("Read", policy => policy.RequireClaim("Read"));
            });

            return services;
        }
    }
}
