using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LocationService> _logger;

    public LocationService(ILocationRepository locationRepository, IUnitOfWork unitOfWork, ILogger<LocationService> logger)
    {
        _locationRepository = locationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new Location in the system.
    /// </summary>
    /// <param name="location">The Location entity to create.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A Result indicating success or failure.</returns>
    ///
    public async Task<Result<Location>> CreateLocationAsync(Location location, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating a Location with Id: {LocationId}", location.Id);

            await _locationRepository.InsertAsync(location, cancellationToken);
            var locationCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (locationCreated)
            {
                _logger.LogInformation("Successfully created a Location with Id: {LocationId}", location.Id);
                return Result<Location>.Success(location); // Return the created Location object
            }

            _logger.LogWarning("Failed to create a Location with Id: {LocationId}", location.Id);
            return Result<Location>.Failure(LocationErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Location with Id: {LocationId}",
                             nameof(LocationService), location.Id);
            return Result<Location>.Failure(LocationErrors.CreationUnexpectedError);
        }
    }

    /// <summary>
    /// Retrieves all Locations from the repository.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A Result containing a collection of Locations or an error.</returns>
    public async Task<Result<IEnumerable<Location>>> GetAllLocationsAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all locations.");

            var locations = await _locationRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved all locations.");
            return Result<IEnumerable<Location>>.Success(locations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all locations.",
                             nameof(LocationService));
            return Result<IEnumerable<Location>>.Failure(LocationErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Retrieves a Location by its ID.
    /// </summary>
    /// <param name="locationId">The ID of the Location to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A Result containing the Location or an error.</returns>
    public async Task<Result<Location>> GetLocationByIdAsync(int locationId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Location with Id: {LocationId}", locationId);

            var location = await _locationRepository.GetByIdAsync(locationId, cancellationToken);

            if (location is null)
            {
                _logger.LogWarning("Location with Id: {LocationId} was not found.", locationId);
                return Result<Location>.Failure(LocationErrors.NotFound(locationId));
            }

            _logger.LogInformation("Successfully retrieved Location with Id: {LocationId}", locationId);
            return Result<Location>.Success(location);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Location with Id: {LocationId}",
                             nameof(LocationService), locationId);
            return Result<Location>.Failure(LocationErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Updates an existing Location in the system.
    /// </summary>
    /// <param name="location">The Location entity to update.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A Result indicating success or failure.</returns>
    public async Task<Result<bool>> UpdateLocationAsync(Location location, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating Location with Id: {LocationId}", location.Id);

            await _locationRepository.UpdateAsync(location, cancellationToken);
            var locationUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (locationUpdated)
            {
                _logger.LogInformation("Successfully updated Location with Id: {LocationId}", location.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to update Location with Id: {LocationId}. No changes were detected.", location.Id);
            return Result<bool>.Failure(LocationErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Location with Id: {LocationId}",
                             nameof(LocationService), location.Id);
            return Result<bool>.Failure(LocationErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a Location by its ID.
    /// </summary>
    /// <param name="locationId">The ID of the Location to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A Result indicating success or failure.</returns>
    public async Task<Result<bool>> DeleteLocationAsync(int locationId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting Location with Id: {LocationId}", locationId);

            var location = await _locationRepository.GetByIdAsync(locationId, cancellationToken);
            if (location is null)
            {
                _logger.LogWarning("Location with Id: {LocationId} was not found.", locationId);
                return Result<bool>.Failure(LocationErrors.NotFound(locationId));
            }

            await _locationRepository.DeleteAsync(location, cancellationToken);
            var locationDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (locationDeleted)
            {
                _logger.LogInformation("Successfully deleted Location with Id: {LocationId}", locationId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to delete Location with Id: {LocationId}. No changes were detected.", locationId);
            return Result<bool>.Failure(LocationErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Location with Id: {LocationId}",
                             nameof(LocationService), locationId);
            return Result<bool>.Failure(LocationErrors.DeletionUnexpectedError);
        }
    }
}
