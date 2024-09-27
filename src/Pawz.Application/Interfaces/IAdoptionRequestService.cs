using Pawz.Application.Models;
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
    Task<Result<bool>> CreateAdoptionRequestAsync(AdoptionRequestCreateRequest adoptionRequestCreateRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all adoptions.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of adoption entities.</returns>
    Task<Result<IEnumerable<AdoptionRequest>>> GetAllAdoptionRequestsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an adoption by its Id.
    /// </summary>
    /// <param name="adoptionRequestId">The Id of the pet to retrieve.</param>
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
    /// Deletes an adoption by its Id.
    /// </summary>
    /// <param name="adoptionRequestId">The Id of the adoption to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteAdoptionRequestAsync(int adoptionRequestId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all adoption requests for a specific pet by its Id.
    /// </summary>
    /// <param name="petId">The Id of the pet.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of <see cref="AdoptionRequest"/> objects.</returns>
    Task<Result<List<AdoptionRequestResponse>>> GetAdoptionRequestsByPetIdAsync(int petId, CancellationToken cancellationToken);

    /// <summary>
    /// Accepts a specific adoption request and automatically rejects all other pending requests for the same pet.
    /// </summary>
    /// <param name="adoptionRequestId">The ID of the adoption request to be accepted.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// Returns a success result if the adoption request was successfully accepted and other requests were rejected.
    /// Returns a failure result if the request was not found or an error occurred during the process.
    /// </returns>
    Task<Result<bool>> AcceptAdoptionRequestAsync(int adoptionRequestId, CancellationToken cancellationToken);

    /// <summary>
    /// Rejects a specific adoption request by setting its status to "Rejected".
    /// </summary>
    /// <param name="adoptionRequestId">The ID of the adoption request to be rejected.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// Returns a success result if the adoption request status was successfully set to "Rejected".
    /// Returns a failure result if the request was not found or an error occurred during the update process.
    /// </returns>
    Task<Result<bool>> RejectAdoptionRequestAsync(int adoptionRequestId, CancellationToken cancellationToken);
    /// <summary>
    /// Checks if a user has already made an adoption request for a specific pet.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="petId">The unique identifier of the pet.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if necessary.</param>
    /// <returns>
    /// A Task representing the asynchronous operation. The task result is a Result<bool> where:
    /// </returns>
    Task<Result<bool>> HasUserMadeRequestForPetAsync(int petId, CancellationToken cancellationToken);
}
