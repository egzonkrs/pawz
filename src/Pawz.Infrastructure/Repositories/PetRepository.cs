using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Helpers;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Common;
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
    /// <param name="cancellationToken"></param>
    /// <returns>An <see cref="IQueryable{Pet}"/> containing pets with their related entities.</returns>
    public async Task<List<Pet>> GetAllPetsWithDetailsAsync(QueryParams queryParams, CancellationToken cancellationToken)
    {
        var query = _dbSet
            .AsNoTracking()
            .Select(p => new Pet
            {
                Id = p.Id,
                Name = p.Name,
                AgeYears = p.AgeYears,
                About = p.About,
                Price = p.Price,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                BreedId = p.BreedId,
                LocationId = p.LocationId,
                PostedByUserId = p.PostedByUserId,
                Breed = new Breed
                {
                    Id = p.Breed.Id,
                    Name = p.Breed.Name,
                    SpeciesId = p.Breed.SpeciesId,
                    Species = new Species
                    {
                        Id = p.Breed.Species.Id,
                        Name = p.Breed.Species.Name,
                        Description = p.Breed.Species.Description
                    }
                },
                Location = new Location
                {
                    Id = p.Location.Id,
                    Address = p.Location.Address,
                    PostalCode = p.Location.PostalCode,
                    CityId = p.Location.CityId,
                    City = new City
                    {
                        Id = p.Location.City.Id,
                        Name = p.Location.City.Name,
                        CountryId = p.Location.City.CountryId,
                        Country = new Country
                        {
                            Id = p.Location.City.Country.Id,
                            Name = p.Location.City.Country.Name
                        }
                    }
                },
                PetImages = p.PetImages.Select(pi => new PetImage
                {
                    Id = pi.Id,
                    ImageUrl = pi.ImageUrl,
                    IsPrimary = pi.IsPrimary,
                    PetId = pi.PetId
                }).ToList(),
                User = new ApplicationUser
                {
                    Id = p.User.Id,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Email = p.User.Email,
                    UserName = p.User.UserName
                }
            })
            .AsQueryable();

        query = query.ApplyQueryParams(queryParams);

        var generatedSql = query.ToQueryString(); // For debugging only!
        var result = await query.ToListAsync(cancellationToken);

        return result;
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
