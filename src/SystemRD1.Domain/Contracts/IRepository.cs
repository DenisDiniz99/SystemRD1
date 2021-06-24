using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Domain.Contracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangeAsync();
    }
}
