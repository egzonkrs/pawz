using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface IAdoptionRequestRepository : IGenericRepository<AdoptionRequest, int>
{
    /// <summary>
    /// Retrieves adoption requests based on their status.
    /// </summary>
    /// <param name="status">The status of the adoption requests to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of adoption requests with the specified status.</returns>
    Task<IEnumerable<AdoptionRequest>> GetRequestsByStatusAsync(AdoptionRequestStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves adoption requests based on the associated pet's ID.
    /// </summary>
    /// <param name="petId">The ID of the pet associated with the adoption requests to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of adoption requests associated with the specified pet.</returns>
    Task<IEnumerable<AdoptionRequest>> GetByPetIdAsync(int petId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves adoption requests made by a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user who made the adoption requests.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of adoption requests made by the specified user.</returns>
    Task<IEnumerable<AdoptionRequest>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a list of adoption requests in the database.
    /// </summary>
    /// <param name="adoptionRequests">The list of <see cref="AdoptionRequest"/> objects to be updated.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="ArgumentException">Thrown when the provided list of adoption requests is null or empty.</exception>
    /// <returns>A task representing the asynchronous update operation.</returns>
    Task UpdateListAsync(List<AdoptionRequest> adoptionRequests, CancellationToken cancellationToken);
}

