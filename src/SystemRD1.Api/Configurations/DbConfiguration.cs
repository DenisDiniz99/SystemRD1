using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using SystemRD1.Data.Contexts;

namespace SystemRD1.Api.Configurations
{
    public static class DbConfiguration
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<SystemContext>(option => 
                    option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
