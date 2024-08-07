using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos
{
    public abstract class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext = context;

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}