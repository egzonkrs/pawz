using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}