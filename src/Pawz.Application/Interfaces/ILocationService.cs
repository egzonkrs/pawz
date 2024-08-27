using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface ILocationService
{
    /// <summary>
    /// Creates a new location.
    /// </summary>
    /// <param name="location">The location entity to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> CreateLocationAsync(Location location, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all locations.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of location entities.</returns>
    Task<Result<IEnumerable<Location>>> GetAllLocationsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a location by its ID.
    /// </summary>
    /// <param name="locationId">The ID of the location to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the location entity.</returns>
    Task<Result<Location>> GetLocationByIdAsync(int locationId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing location.
    /// </summary>
    /// <param name="location">The location entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdateLocationAsync(Location location, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a location by its ID.
    /// </summary>
    /// <param name="locationId">The ID of the location to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteLocationAsync(int locationId, CancellationToken cancellationToken);
}
