using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Domain.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> GetDocumentExists(string document);
        Task<bool> CustomerExists(Guid id);
    }
}
