using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity.Id;
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dbSet.Remove(entity));
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dbSet.Update(entity));
        }
    }
}
