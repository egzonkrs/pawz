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
}
