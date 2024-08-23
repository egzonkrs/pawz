using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos
{
    public class PetRepository : GenericRepository<Pet, int>, IPetRepository
    {
        private readonly AppDbContext _context;
        public PetRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pet>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.PetImages)
                .Include(s => s.Species)
                .Include(b => b.Breed)
                // .Include(u => u.User)
                .Include(l => l.Location)
                // .Include(ar => ar.AdoptionRequests)
                .ToListAsync(cancellationToken);
        }
    }
}