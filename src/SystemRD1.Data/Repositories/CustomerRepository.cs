using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Customer> GetByDocument(string document)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Document == document);
        }

        public async Task<IEnumerable<Customer>> GetByName(string name)

        {
            return await _dbSet.Where(c => c.FirstName == name).ToListAsync();
        }

        public async Task<bool> DocumentExists(string document)
        {
            var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Document == document);

            if (result == null) return false;

            return true;
        }

    }
}
