using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity found or null.</returns>
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves all entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously adds a new entity to the context and returns the primary key of the added entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>A task that represents the asynchronous add operation. The task result contains the primary key of the added entity.</returns>
        Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously deletes an entity from the context.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously updates an existing entity in the context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}