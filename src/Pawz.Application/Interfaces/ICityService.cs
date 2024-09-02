using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface ICityService
{
    /// <summary>
    /// Creates a new city.
    /// </summary>
    /// <param name="city">The city entity to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> CreateCityAsync(City city, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all cities.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of city entities.</returns>
    Task<Result<List<City>>> GetAllCitiesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a city by its ID.
    /// </summary>
    /// <param name="cityId">The ID of the city to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the city entity.</returns>
    Task<Result<City>> GetCityByIdAsync(int cityId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing city.
    /// </summary>
    /// <param name="city">The city entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdateCityAsync(City city, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a city by its ID.
    /// </summary>
    /// <param name="cityId">The ID of the city to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteCityAsync(int cityId, CancellationToken cancellationToken);
}
