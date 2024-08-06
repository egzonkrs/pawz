using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity found or null.</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Asynchronously adds a new entity to the context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Asynchronously deletes an entity from the context.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Asynchronously updates an existing entity in the context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAsync(TEntity entity);
    }
}