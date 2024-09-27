using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Domain.Specifications;
using Pawz.Domain.Specifications.PetSpecifications;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Pawz.Infrastructure.Repos;

public class PetRepository : GenericRepository<Pet, int>, IPetRepository
{
    private readonly AppDbContext _context;
    public PetRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pet>> GetAllPetsWithRelatedEntitiesAsync(PetFilterQueryParams filterParams = null,
        CancellationToken cancellationToken = default)
    {
        var spec = new PetsWithAllRelatedEntitiesSpecification(filterParams);
        return await ListAsync(spec, cancellationToken);
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
}
