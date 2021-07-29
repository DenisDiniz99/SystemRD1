using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using SystemRD1.Api.Configurations.Swagger;
using SystemRD1.Api.Data;
using SystemRD1.Api.Services.Email;
using SystemRD1.Business.Contracts.Notifiers;
using SystemRD1.Business.Contracts.Services;
using SystemRD1.Business.Notifications;
using SystemRD1.Business.Services;
using SystemRD1.Data.Contexts;
using SystemRD1.Data.Repositories;
using SystemRD1.Domain.Contracts;

namespace SystemRD1.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            //Context :: SystemContext
            services.AddScoped<SystemContext>();

            //Context :: IdentityContext
            services.AddScoped<IdentityContext>();

            //Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //Services
            services.AddScoped<ICustomerService, CustomerService>();

            //Notifier
            services.AddScoped<INotifier, Notifier>();

            //HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //SenderEmail
            services.AddTransient<IEmailSender, EmailSender>();
            
        }
    }
}
