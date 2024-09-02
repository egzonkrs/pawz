using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface ISpeciesService
{
    /// <summary>
    /// Creates a new species.
    /// </summary>
    /// <param name="species">The species entity to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> CreateSpeciesAsync(Species species, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all species.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of species entities.</returns>
    Task<Result<List<Species>>> GetAllSpeciesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a species by its ID.
    /// </summary>
    /// <param name="speciesId">The ID of the species to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the species entity.</returns>
    Task<Result<Species>> GetSpeciesByIdAsync(int speciesId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing species.
    /// </summary>
    /// <param name="species">The species entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdateSpeciesAsync(Species species, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a species by its ID.
    /// </summary>
    /// <param name="speciesId">The ID of the species to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteSpeciesAsync(int speciesId, CancellationToken cancellationToken);
}
