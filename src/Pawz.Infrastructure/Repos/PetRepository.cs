using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
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
            // .Include(ar => ar.AdoptionRequests)
            .ToListAsync(cancellationToken);
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
}
