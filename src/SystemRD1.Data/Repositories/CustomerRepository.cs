using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using SystemRD1.Data.Contexts;
using SystemRD1.Domain.Contracts;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SystemContext context) : base(context) { }

        public async Task<bool> CustomerExists(Guid id)
        {
            var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (result == null) return false;

            return true;
        }

        public async Task<bool> GetDocumentExists(string document)
        {
            var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Document == document);

            if (result == null) return false;

            return true;
        }

    }
}
