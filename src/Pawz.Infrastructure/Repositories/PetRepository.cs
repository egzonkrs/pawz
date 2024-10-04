using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Helpers;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Pawz.Infrastructure.Repositories;

public class PetRepository : GenericRepository<Pet, int>, IPetRepository
{
    public PetRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a queryable collection of pets with related entities, allowing for further filtering, sorting, or pagination.
    /// </summary>
    /// <param name="queryParams">The parameters used for filtering, sorting, and pagination of pets.</param>
    /// <returns>An <see cref="IQueryable{Pet}"/> containing pets with their related entities.</returns>
    public IQueryable<Pet> GetAllPetsWithRelatedEntitiesAsQueryable(PetQueryParams queryParams)
    {
        IQueryable<Pet> petsQuery = _dbSet
            .AsSplitQuery()
            .Include(p => p.PetImages)
            .Include(p => p.Breed)
            .ThenInclude(b => b.Species)
            .Include(p => p.User)
            .Include(p => p.Location)
            .ThenInclude(l => l.City)
            .ThenInclude(c => c.Country)
            .Include(p => p.AdoptionRequests)
            .AsQueryable();

        if (!string.IsNullOrEmpty(queryParams.SpeciesName))
        {
            petsQuery = petsQuery.Where(p => p.Breed.Species.Name == queryParams.SpeciesName);
        }

        if (!string.IsNullOrEmpty(queryParams.BreedName))
        {
            petsQuery = petsQuery.Where(p => p.Breed.Name == queryParams.BreedName);
        }

        return petsQuery;
    }

    /// <summary>
    /// Asynchronously retrieves all pets along with their related entities.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IEnumerable{Pet}"/> containing all pets with their related entities.</returns>
    public async Task<IEnumerable<Pet>> GetAllPetsWithRelatedEntitiesAsync(CancellationToken cancellationToken = default)
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
    public async Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, CancellationToken cancellationToken = default)
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
}
