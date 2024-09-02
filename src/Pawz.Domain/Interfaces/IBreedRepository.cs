using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface IBreedRepository : IGenericRepository<Breed, int>
{
    Task<IEnumerable<Breed>> GetBreedsBySpeciesIdAsync(int speciesId, CancellationToken cancellationToken);
}
