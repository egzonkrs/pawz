using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repositories;

public class PetRepository : GenericRepository<Pet, int>, IPetRepository
{
    private readonly AppDbContext _context;
    public PetRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously retrieves all pets from the database, including their related entities such as PetImages, Breed, Species, User, and Location.
    /// This method ensures that the associated data is loaded together with the pets to avoid multiple queries or lazy loading issues.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>An IEnumerable of Pet objects with their related entities fully loaded.</returns>
    public async Task<IEnumerable<Pet>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.PetImages)
            .Include(p => p.Breed)
            .ThenInclude(b => b.Species)
            .Include(u => u.User)
            .Include(l => l.Location)
             .ThenInclude(l => l.City)
                .ThenInclude(c => c.Country)
            // .Include(ar => ar.AdoptionRequests)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a single Pet entity by its ID, including all related entities such as PetImages, Breed, Species, User, and Location.
    /// This method uses eager loading to ensure all related entities are loaded in the same query to prevent additional database calls.
    /// </summary>
    /// <param name="id">The unique identifier of the Pet to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the Pet entity with all related entities loaded,
    /// or null if no pet with the specified ID is found.
    /// </returns>
    public async Task<Pet> GetPetByIdWithRelatedEntitiesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.PetImages)
            .Include(p => p.Breed)
                .ThenInclude(b => b.Species)
            .Include(p => p.User)
            .Include(p => p.Location)
                .ThenInclude(p => p.City)
                    .ThenInclude(p => p.Country)
            //TODO .Include(p => p.AdoptionRequests)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<int> CountPetsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all pets associated with a specific user by their unique user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose pets are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of pets associated with the specified user, including related location, breed, and images.</returns>
    public async Task<IEnumerable<Pet>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(pet => pet.PostedByUserId == userId)
            .Include(pet => pet.Location)
            .ThenInclude(location => location.City)
            .Include(pet => pet.Breed)
            .Include(pet => pet.PetImages)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all pets associated with a specific user by their unique user ID, including the related user details.
    /// </summary>
    /// <param name="userId">The ID of the user whose pets are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of pets associated with the specified user, including related location, breed, images, and user details.</returns>
    public async Task<IEnumerable<Pet>> GetPetsByUserIdWithUserDetailsAsync(string userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .AsSplitQuery()
            .Where(pet => pet.PostedByUserId == userId)
            .Include(p => p.Location)
                .ThenInclude(p => p.City)
                    .ThenInclude(p => p.Country)
            .Include(pet => pet.Breed)
            .Include(pet => pet.PetImages)
            .Include(pet => pet.User)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of pets posted by a specific user along with the total count of their pets.
    /// </summary>
    /// <param name="userId">The ID of the user to filter pets by.</param>
    /// <param name="page">The current page number for pagination.</param>
    /// <param name="pageSize">The number of pets to return per page.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A tuple containing the list of pets and the total count of pets posted by the user.</returns>
    public async Task<(IEnumerable<Pet> Pets, int TotalCount)> GetPetsByUserIdWithPaginationAsync(
        string userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await GetTotalPetsCountForUserAsync(userId, cancellationToken);
        var pets = await GetPaginatedPetsQuery(userId, page, pageSize).ToListAsync(cancellationToken);

        return (pets, totalCount);
    }

    /// <summary>
    /// Retrieves the total number of pets posted by a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user to filter pets by.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The total number of pets posted by the user.</returns>
    public async Task<int> GetTotalPetsCountForUserAsync(string userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(p => p.PostedByUserId == userId)
            .CountAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated query for pets posted by a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user to filter pets by.</param>
    /// <param name="page">The current page number for pagination.</param>
    /// <param name="pageSize">The number of pets to return per page.</param>
    /// <returns>An <see cref="IQueryable{Pet}"/> representing the paginated query of pets.</returns>
    private IQueryable<Pet> GetPaginatedPetsQuery(string userId, int page, int pageSize)
    {
        return _dbSet
            .AsNoTracking()
            .Where(pet => pet.PostedByUserId == userId)
            .Include(pet => pet.Location)
                .ThenInclude(location => location.City)
            .Include(pet => pet.Breed)
            .Include(pet => pet.PetImages)
            .OrderByDescending(pet => pet.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}
