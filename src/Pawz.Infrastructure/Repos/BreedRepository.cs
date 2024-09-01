using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos;

public class BreedRepository : GenericRepository<Breed, int>, IBreedRepository
{
    public BreedRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves all breeds associated with a specific species by its Id.
    /// </summary>
    /// <param name="speciesId">The Id of the species whose breeds should be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of breeds associated with the specified species.</returns>

    public async Task<IEnumerable<Breed>> GetBreedsBySpeciesIdAsync(int speciesId, CancellationToken cancellationToken)
    {
        return await _dbSet
             .Where(b => b.SpeciesId == speciesId)
             .ToListAsync(cancellationToken);
    }
}
