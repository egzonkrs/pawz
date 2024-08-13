using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces
{
    public interface IAdoptionService
    {
        /// <summary>
        /// Creates a new adoption.
        /// </summary>
        /// <param name="adoption">The pet entity to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
        Task<Result<bool>> CreateAdoptionAsync(Adoption adoption, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all adoptions.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a collection of adoption entities.</returns>
        Task<Result<IEnumerable<Adoption>>> GetAllAdoptionsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves an adoption by its ID.
        /// </summary>
        /// <param name="adoptionId">The ID of the pet to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains the adoption entity.</returns>
        Task<Result<Adoption>> GetAdoptionByIdAsync(int adoptionId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing adoption.
        /// </summary>
        /// <param name="adoption">The adoption entity to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
        Task<Result<bool>> UpdateAdoptionAsync(Adoption adoption, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an adoption by its ID.
        /// </summary>
        /// <param name="adoptionId">The ID of the adoption to delete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
        Task<Result<bool>> DeleteAdoptionAsync(int adoptionId, CancellationToken cancellationToken);
    }
}
