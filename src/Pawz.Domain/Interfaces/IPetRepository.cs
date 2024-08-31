using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces
{
    public interface IPetRepository : IGenericRepository<Pet, int>
    {
        Task<IEnumerable<Pet>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default);
        Task<int> CountPetsAsync(CancellationToken cancellationToken = default);
    }
}