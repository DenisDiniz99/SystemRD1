using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Domain.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> DocumentExists(string document);
        Task<bool> CustomerExists(Guid id);
        Task<IEnumerable<Customer>> GetByName(string name);
        Task<Customer> GetByDocument(string document);
    }
}
