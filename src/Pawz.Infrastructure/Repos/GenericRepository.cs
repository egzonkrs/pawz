using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos
{
    public abstract class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext = context;

        public async Task<TEntity> GetById(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}