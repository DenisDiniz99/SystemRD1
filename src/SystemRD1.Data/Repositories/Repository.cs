using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemRD1.Data.Contexts;
using SystemRD1.Domain.Contracts;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly SystemContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(SystemContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChangeAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChangeAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            await SaveChangeAsync();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
