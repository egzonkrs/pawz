using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Common.Specifications;
using Pawz.Domain.Interfaces;
using Pawz.Domain.Specifications;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos;

public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    private readonly AppDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(AppDbContext context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<TEntity>();
    }

    /// <summary>
    /// Retrieves an entity by its Id.
    /// </summary>
    /// <param name="id">The Id of the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>The entity with the specified Id, or null if not found.</returns>
    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    /// <summary>
    /// Retrieves all entities of the specified type.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>A list of all entities of the specified type.</returns>
    public async Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Inserts a new entity into the database.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>The Id of the inserted entity.</returns>
    public async Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_dbSet.Remove(entity));
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_dbSet.Update(entity));
    }

    public async Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbSet.AsQueryable(), spec);
    }
}
