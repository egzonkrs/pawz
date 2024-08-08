using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces
{
    public interface IPetService
    {
        /// <summary>
        /// Asynchronously creates a new pet.
        /// </summary>
        /// <param name="pet">The pet entity to create.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> CreatePetAsync(Pet pet);

        /// <summary>
        /// Asynchronously retrieves all pets.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of pet entities.</returns>
        Task<IEnumerable<Pet>> GetAllPetsAsync();

        /// <summary>
        /// Asynchronously retrieves a pet by its ID.
        /// </summary>
        /// <param name="petId">The ID of the pet to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the pet entity.</returns>
        Task<Pet> GetPetByIdAsync(int petId);

        /// <summary>
        /// Asynchronously updates an existing pet.
        /// </summary>
        /// <param name="pet">The pet entity to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> UpdatePetAsync(Pet pet);

        /// <summary>
        /// Asynchronously deletes a pet by its ID.
        /// </summary>
        /// <param name="petId">The ID of the pet to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> DeletePetAsync(int petId);
    }
}