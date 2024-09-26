using Pawz.Application.Models;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.PetModels;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IPetService
{
    /// <summary>
    /// Creates a new pet.
    /// </summary>
    /// <param name="pet">The pet entity to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<int>> CreatePetAsync(PetCreateRequest petCreateRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all pets.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of pet entities.</returns>
    Task<Result<IEnumerable<Pet>>> GetAllPetsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a pet by its ID.
    /// </summary>
    /// <param name="petId">The ID of the pet to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the pet entity.</returns>
    Task<Result<PetResponse>> GetPetByIdAsync(int petId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing pet.
    /// </summary>
    /// <param name="pet">The pet entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdatePetAsync(Pet pet, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a pet by its ID.
    /// </summary>
    /// <param name="petId">The ID of the pet to delete.</param>
    /// <param name="userId">The ID of the user who created the pet.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeletePetAsync(int petId, string userId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all pets created by a specific user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of pet entities.</returns>
    Task<Result<IEnumerable<UserPetResponse>>> GetPetsByUserIdAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all pets along with their associated related entities.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the operation. The task result contains an <see cref="IEnumerable{Pet}"/> with all pets and their related entities, or an error if the operation fails.</returns>
    Task<Result<IEnumerable<PetResponse>>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of pets for the current user along with the total count of pets.
    /// </summary>
    /// <param name="page">The current page number for pagination.</param>
    /// <param name="pageSize">The number of pets to be returned per page.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A Result containing:
    /// 1. A tuple with an IEnumerable of UserPetResponse representing the paginated list of pets.
    /// 2. The total count of pets.
    /// </returns>
    Task<Result<(IEnumerable<UserPetResponse> Pets, int TotalCount)>> GetPetsByUserIdWithPaginationAsync(
    int page, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the total count of pets for the current user.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> 
    /// indicating the total number of pets posted by the user.
    /// </returns>
    Task<Result<int>> GetTotalPetsCountForUserAsync(CancellationToken cancellationToken);
}

