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
    Task<Result<bool>> CreatePetAsync(PetCreateRequest petCreateRequest, CancellationToken cancellationToken);

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
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeletePetAsync(int petId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all pets created by a specific user.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of pet entities.</returns>
    Task<Result<IEnumerable<UserPetResponse>>> GetPetsByUserIdAsync(string searchTerm,CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all pets along with their associated related entities.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the operation. The task result contains an <see cref="IEnumerable{Pet}"/> with all pets and their related entities, or an error if the operation fails.</returns>
    Task<Result<IEnumerable<PetResponse>>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default);
}

