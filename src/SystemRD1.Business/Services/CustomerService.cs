using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Business.Contracts.Notifiers;
using SystemRD1.Business.Contracts.Services;
using SystemRD1.Domain.Contracts;
using SystemRD1.Domain.Entities;
using SystemRD1.Domain.Validations;

namespace SystemRD1.Business.Services
{
    public class CustomerService : Service, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, INotifier notifier) : base(notifier)
        {
            _customerRepository = customerRepository;
        }

        public async Task AddAsync(Customer customer)
        {
            if (!PerformValidation(new CustomerValidation(), customer)) return;        

            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            if (!PerformValidation(new CustomerValidation(), customer)) return;

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);   
        }

    }
}
