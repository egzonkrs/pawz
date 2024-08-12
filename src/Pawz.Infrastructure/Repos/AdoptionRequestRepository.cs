using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enum;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos;
public class AdoptionRequestRepository : GenericRepository<AdoptionRequest, int>, IAdoptionRequestRepository
{
    public AdoptionRequestRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AdoptionRequest>> GetRequestsByStatusAsync(PetStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ar => ar.Status == status.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AdoptionRequest>> GetByPetIdAsync(int petId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ar => ar.PetId == petId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AdoptionRequest>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ar => ar.RequesterUserId == userId)
            .ToListAsync(cancellationToken);
    }
}
