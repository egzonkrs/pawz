using Pawz.Application.Models;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.PetModels;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Helpers;
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
    /// Asynchronously retrieves all pets along with their related entities.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation,
    /// containing a <see cref="Result{T}"/> of an <see cref="IEnumerable{PetResponse}"/> with pets and their related entities.</returns>
    Task<Result<IEnumerable<PetResponse>>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a filtered collection of pets with related entities based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The parameters used for filtering, sorting, and pagination of pets.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation,
    /// containing a <see cref="Result{T}"/> of an <see cref="IEnumerable{PetResponse}"/> with pets and their related entities.</returns>
    Task<Result<IEnumerable<PetResponse>>> GetAllPetsWithRelatedEntities(QueryParams queryParams, CancellationToken cancellationToken = default);
}

