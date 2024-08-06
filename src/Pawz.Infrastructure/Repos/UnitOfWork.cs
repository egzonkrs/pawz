using Pawz.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}