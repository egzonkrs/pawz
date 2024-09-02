using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IBreedService
{
    /// <summary>
    /// Creates a new breed.
    /// </summary>
    /// <param name="breed">The breed entity to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> CreateBreedAsync(Breed breed, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all breeds.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of breed entities.</returns>
    Task<Result<List<Breed>>> GetAllBreedsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a breed by its ID.
    /// </summary>
    /// <param name="breedId">The ID of the breed to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the breed entity.</returns>
    Task<Result<Breed>> GetBreedByIdAsync(int breedId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing breed.
    /// </summary>
    /// <param name="breed">The breed entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdateBreedAsync(Breed breed, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a breed by its ID.
    /// </summary>
    /// <param name="breedId">The ID of the breed to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteBreedAsync(int breedId, CancellationToken cancellationToken);
}
