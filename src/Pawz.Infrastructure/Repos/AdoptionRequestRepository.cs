using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
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

    /// <summary>
    /// Retrieves adoption requests based on their status.
    /// </summary>
    /// <param name="status">The status of the adoption requests to filter by.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>A list of adoption requests matching the specified status.</returns>
    public async Task<IEnumerable<AdoptionRequest>> GetRequestsByStatusAsync(AdoptionRequestStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ar => ar.Status == status)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all adoption requests associated with a specific pet by its Id.
    /// </summary>
    /// <param name="petId">The Id of the pet whose adoption requests should be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>A list of adoption requests associated with the specified pet.</returns>
    public async Task<IEnumerable<AdoptionRequest>> GetByPetIdAsync(int petId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ar => ar.PetId == petId)
            .Include(ar => ar.User) // Include për të marrë të dhënat e përdoruesit që ka bërë kërkesën
            .Include(ar => ar.Pet)
            .Include(ar => ar.Pet.Breed)
            .Include(ar => ar.Pet.Breed.Species)
            .Include(ar => ar.Pet.Location.City)
            .Include(ar => ar.Pet.PetImages)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all adoption requests submitted by a specific user based on their user Id.
    /// </summary>
    /// <param name="userId">The Id of the user whose adoption requests should be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>A list of adoption requests submitted by the specified user.</returns>
    public async Task<IEnumerable<AdoptionRequest>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ar => ar.RequesterUserId == userId)
            .ToListAsync(cancellationToken);
    }
}
