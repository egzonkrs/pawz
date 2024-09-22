using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System;
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
    /// <exception cref="ArgumentException">Thrown when the provided list of adoption requests is null or empty.</exception>
    /// <returns>A task representing the asynchronous update operation.</returns>
    public async Task UpdateListAsync(List<AdoptionRequest> adoptionRequests, CancellationToken cancellationToken)
    {
        if (adoptionRequests == null || adoptionRequests.Count == 0)
            throw new ArgumentException("No adoption requests provided for update.");

        foreach (var adoptionRequest in adoptionRequests)
        {
            if (_dbSet.Local.All(ar => ar.Id != adoptionRequest.Id))
            {
                _dbSet.Attach(adoptionRequest);
            }

            _dbSet.Entry(adoptionRequest).State = EntityState.Modified;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
