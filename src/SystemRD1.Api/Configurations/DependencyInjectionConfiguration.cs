using Microsoft.Extensions.DependencyInjection;
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
        public static void AddDependencyInjectionConfiguration(this IServiceCollection service)
        {
            //Repositories
            service.AddScoped<ICustomerRepository, CustomerRepository>();

            //Services
            service.AddScoped<ICustomerService, CustomerService>();

            //Notifier
            service.AddScoped<INotifier, Notifier>();

            //Context
            service.AddScoped<SystemContext>();
        }
    }
}
