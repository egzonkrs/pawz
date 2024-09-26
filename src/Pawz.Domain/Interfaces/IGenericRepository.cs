using Pawz.Domain.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

/// <summary>
/// A generic repository to handle all the basic operations like create, read, update and delete.
/// </summary>
/// <typeparam name="TEntity">The entity that we're modifying the data.</typeparam>
/// <typeparam name="TKey">The key of the entity that we're modifying the data.</typeparam>
public interface IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// Retrieves an entity by its primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all the records of the given entity.
    /// </summary>
    Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity to the context and returns the primary key of the added entity.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity from the context.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity in the context.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a single entity that matches the given specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="spec">The specification used to filter the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity that matches the specification.</returns>
    Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> spec);

    /// <summary>
    /// Retrieves a read-only list of entities that match the given specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities.</typeparam>
    /// <param name="spec">The specification used to filter the entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of entities that match the specification.</returns>
    Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);

}
