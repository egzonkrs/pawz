using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IAdoptionRequestService
{
    /// <summary>
    /// Creates a new adoption.
    /// </summary>
    /// <param name="adoptionRequest">The pet entity to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> CreateAdoptionRequestAsync(AdoptionRequest adoptionRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all adoptions.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of adoption entities.</returns>
    Task<Result<IEnumerable<AdoptionRequest>>> GetAllAdoptionRequestsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an adoption by its ID.
    /// </summary>
    /// <param name="adoptionRequestId">The ID of the pet to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the adoption entity.</returns>
    Task<Result<AdoptionRequest>> GetAdoptionRequestByIdAsync(int adoptionRequestId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing adoption.
    /// </summary>
    /// <param name="adoptionRequest">The adoption entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdateAdoptionRequestAsync(AdoptionRequest adoptionRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an adoption by its ID.
    /// </summary>
    /// <param name="adoptionRequestId">The ID of the adoption to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteAdoptionRequestAsync(int adoptionRequestId, CancellationToken cancellationToken);
}
