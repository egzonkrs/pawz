using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pawz.Domain.Helpers;
using System.Linq;

namespace Pawz.Domain.Interfaces;

public interface IPetRepository : IGenericRepository<Pet, int>
{
    /// <summary>
    /// Asynchronously retrieves all pets along with their related entities.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IEnumerable{Pet}"/> containing all pets with their related entities.</returns>
    Task<IEnumerable<Pet>> GetAllPetsWithRelatedEntitiesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a queryable collection of pets with related entities, allowing for further filtering, sorting, or pagination.
    /// </summary>
    /// <param name="queryParams">The parameters used for filtering, sorting, and pagination of pets.</param>
    /// <returns>An <see cref="IQueryable{Pet}"/> containing pets with their related entities.</returns>
    public IQueryable<Pet> GetAllPetsWithRelatedEntitiesAsQueryable(QueryParams queryParams);

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
    Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all pets associated with a specific user by their unique user ID, including the details of the user.
    /// This can be used to fetch both the pets and user information in one operation.
    /// </summary>
    /// <param name="userId">The ID of the user whose pets are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of pets associated with the specified user, along with the user's details.</returns>
    Task<IEnumerable<Pet>> GetPetsByUserIdWithUserDetailsAsync(string userId, CancellationToken cancellationToken);
}
