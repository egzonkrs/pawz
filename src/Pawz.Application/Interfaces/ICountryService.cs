using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface ICountryService
{
    /// <summary>
    /// Creates a new country.
    /// </summary>
    /// <param name="country">The country entity to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> CreateCountryAsync(Country country, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all countries.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of country entities.</returns>
    Task<Result<List<Country>>> GetAllCountriesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a country by its ID.
    /// </summary>
    /// <param name="countryId">The ID of the country to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the country entity.</returns>
    Task<Result<Country>> GetCountryByIdAsync(int countryId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing country.
    /// </summary>
    /// <param name="country">The country entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdateCountryAsync(Country country, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a country by its ID.
    /// </summary>
    /// <param name="countryId">The ID of the country to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteCountryAsync(int countryId, CancellationToken cancellationToken);
}
