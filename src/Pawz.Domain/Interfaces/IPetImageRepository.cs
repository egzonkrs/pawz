using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

/// <summary>
/// Interface for the repository that manages pet images.
/// </summary>
public interface IPetImageRepository : IGenericRepository<PetImage, int>
{
    /// <summary>
    /// Inserts a new pet image into the repository asynchronously.
    /// </summary>
    /// <param name="petImage">The pet image entity to be inserted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The ID of the inserted pet image.</returns>
    Task<int> InsertAsync(PetImage petImage, CancellationToken cancellationToken);
}
