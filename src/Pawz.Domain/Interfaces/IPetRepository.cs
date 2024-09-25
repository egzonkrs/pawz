using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface IPetRepository : IGenericRepository<Pet, int>
{

    /// <summary>
    /// Retrieves all pets along with their related entities, including images, breed, species, user, and location.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of all pets with their related entities.</returns>
    Task<IEnumerable<Pet>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the total number of pets available in the database.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The total count of pets.</returns>
    Task<int> CountPetsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all pets associated with a specific user by their unique user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose pets are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of pets associated with the specified user.</returns>
    Task<IEnumerable<Pet>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a single Pet entity by its unique ID, including all related entities such as PetImages, Breed, Species, User, and Location.
    /// This method uses eager loading to fetch the associated data in a single query to avoid multiple round-trips to the database.
    /// </summary>
    /// <param name="id">The unique identifier of the Pet to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the Pet entity with all related entities included, 
    /// or null if no pet with the specified ID is found.
    /// </returns>
    Task<Pet> GetPetByIdWithRelatedEntitiesAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all pets associated with a specific user by their unique user ID, including the details of the user.
    /// This can be used to fetch both the pets and user information in one operation.
    /// </summary>
    /// <param name="userId">The ID of the user whose pets are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of pets associated with the specified user, along with the user's details.</returns>
    Task<IEnumerable<Pet>> GetPetsByUserIdWithUserDetailsAsync(string userId, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a paginated list of pets for a specific user using cursor-based pagination.
    /// </summary>
    /// <param name="userId">The ID of the user whose pets are being retrieved.</param>
    /// <param name="pageSize">The maximum number of pets to return per page.</param>
    /// <param name="cursor">The cursor (pet ID) from which to start the next set of pets, or null for the first page.</param>
    /// <param name="cancellationToken">A cancellation token to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a tuple
    /// with a collection of pets and the next cursor, which is the ID of the last pet 
    /// in the list if more pets are available.
    /// </returns>
    Task<(IEnumerable<Pet> Pets, string NextCursor)> GetPetsByUserIdWithCursorPaginationAsync(
    string userId, int pageSize, string cursor, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the total count of pets for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user whose pet count is being retrieved.</param>
    /// <param name="cancellationToken">A cancellation token to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the total
    /// number of pets posted by the user.
    /// </returns>
    Task<int> GetTotalPetsCountForUserAsync(string userId, CancellationToken cancellationToken);



}
