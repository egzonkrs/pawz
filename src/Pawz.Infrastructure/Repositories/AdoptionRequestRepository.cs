using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repositories;

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
    /// Retrieves an adoption request by its Id, including the associated pet and its related entities.
    /// </summary>
    /// <param name="id">The Id of the adoption request to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>The adoption request with the specified Id, including its associated pet, or null if not found.</returns>
    public async Task<AdoptionRequest> GetByAdoptionIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(ar => ar.Pet)
                .ThenInclude(p => p.Breed)
                .ThenInclude(b => b.Species)
            .Include(ar => ar.Pet)
                .ThenInclude(p => p.Location)
                .ThenInclude(l => l.City)
            .Include(ar => ar.Pet)
                .ThenInclude(p => p.PetImages)
            .FirstOrDefaultAsync(ar => ar.Id == id, cancellationToken);
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
            .Include(ar => ar.User)
            .Include(ar => ar.Pet)
               .ThenInclude(p => p.Breed)
               .ThenInclude(b => b.Species)
            .Include(ar => ar.Pet)
               .ThenInclude(p => p.Location)
               .ThenInclude(l => l.City)
            .Include(ar => ar.Pet)
               .ThenInclude(p => p.PetImages)
            .AsSplitQuery()
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

    /// <summary>
    /// Updates a list of adoption requests in the database.
    /// </summary>
    /// <param name="adoptionRequests">The list of <see cref="AdoptionRequest"/> objects to be updated.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    public Task UpdateListAsync(List<AdoptionRequest> adoptionRequests, CancellationToken cancellationToken)
    {
        _dbSet.UpdateRange(adoptionRequests);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Checks if an adoption request exists for a given user and pet in the database.
    /// </summary>
    /// <param name="userId">The ID of the user making the request.</param>
    /// <param name="petId">The ID of the pet for which the request is being made.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation, containing a boolean indicating whether an adoption request exists.</returns>
    public async Task<bool> ExistsByUserIdAndPetIdAsync(string userId, int petId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AnyAsync(ar => ar.RequesterUserId == userId && ar.PetId == petId, cancellationToken);
    }
}
