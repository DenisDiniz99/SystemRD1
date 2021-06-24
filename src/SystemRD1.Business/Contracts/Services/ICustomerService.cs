using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Business.Contracts.Services
{
    public interface ICustomerService
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);
    }
}
