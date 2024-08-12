using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces
{
    public interface IPetService
    {
        /// <summary>
        /// Creates a new pet.
        /// </summary>
        /// <param name="pet">The pet entity to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> CreatePetAsync(Pet pet, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all pets.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a collection of pet entities.</returns>
        Task<IEnumerable<Pet>> GetAllPetsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a pet by its ID.
        /// </summary>
        /// <param name="petId">The ID of the pet to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains the pet entity.</returns>
        Task<Pet> GetPetByIdAsync(int petId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing pet.
        /// </summary>
        /// <param name="pet">The pet entity to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> UpdatePetAsync(Pet pet, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a pet by its ID.
        /// </summary>
        /// <param name="petId">The ID of the pet to delete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> DeletePetAsync(int petId, CancellationToken cancellationToken);
    }
}
