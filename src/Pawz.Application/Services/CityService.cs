using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityService> _logger;

    public CityService(ICityRepository cityRepository, IUnitOfWork unitOfWork, ILogger<CityService> logger)
    {
        _cityRepository = cityRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new city in the system.
    /// </summary>
    /// <param name="city">The city entity to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the creation was successful.</returns>
    public async Task<Result<bool>> CreateCityAsync(City city, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating a City with Id: {CityId}", city.Id);

            await _cityRepository.InsertAsync(city, cancellationToken);
            var cityCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (cityCreated)
            {
                _logger.LogInformation("Successfully created a City with Id: {CityId}", city.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to create a City with Id: {CityId}", city.Id);
            return Result<bool>.Failure(CityErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a City with Id: {CityId}",
                             nameof(CityService), city.Id);
            return Result<bool>.Failure(CityErrors.CreationUnexpectedError);
        }
    }

    /// <summary>
    /// Retrieves all cities from the system.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing a collection of all cities.</returns>
    public async Task<Result<List<City>>> GetAllCitiesAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all cities.");

            var cities = await _cityRepository.GetAllAsync(cancellationToken);
            if (cities is null)
            {
                _logger.LogInformation("Successfully retrieved all cities.");
                return Result<List<City>>.Failure(CityErrors.RetrievalError);
            }

            _logger.LogInformation("Successfully retrieved all cities.");
            return Result<List<City>>.Success(cities.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all cities.", nameof(CityService));
            return Result<List<City>>.Failure(CityErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Retrieves a city by its unique ID.
    /// </summary>
    /// <param name="cityId">The ID of the city to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the city entity if found, or an error if not found.</returns>
    public async Task<Result<City>> GetCityByIdAsync(int cityId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving City with Id: {CityId}", cityId);

            var city = await _cityRepository.GetByIdAsync(cityId, cancellationToken);

            if (city is null)
            {
                _logger.LogWarning("City with Id: {CityId} was not found.", cityId);
                return Result<City>.Failure(CityErrors.NotFound(cityId));
            }

            _logger.LogInformation("Successfully retrieved City with Id: {CityId}", cityId);
            return Result<City>.Success(city);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve City with Id: {CityId}",
                             nameof(CityService), cityId);
            return Result<City>.Failure(CityErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Updates an existing city in the system.
    /// </summary>
    /// <param name="city">The city entity to update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the update was successful.</returns>
    public async Task<Result<bool>> UpdateCityAsync(City city, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating City with Id: {CityId}", city.Id);

            await _cityRepository.UpdateAsync(city, cancellationToken);
            var cityUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (cityUpdated)
            {
                _logger.LogInformation("Successfully updated City with Id: {CityId}", city.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to update City with Id: {CityId}. No changes were detected.", city.Id);
            return Result<bool>.Failure(CityErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update City with Id: {CityId}",
                             nameof(CityService), city.Id);
            return Result<bool>.Failure(CityErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a city by its unique ID.
    /// </summary>
    /// <param name="cityId">The ID of the city to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the deletion was successful.</returns>
    public async Task<Result<bool>> DeleteCityAsync(int cityId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting City with Id: {CityId}", cityId);

            var city = await _cityRepository.GetByIdAsync(cityId, cancellationToken);

            if (city is null)
            {
                _logger.LogWarning("City with Id: {CityId} was not found.", cityId);
                return Result<bool>.Failure(CityErrors.NotFound(cityId));
            }

            await _cityRepository.DeleteAsync(city, cancellationToken);
            var cityDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (cityDeleted)
            {
                _logger.LogInformation("Successfully deleted City with Id: {CityId}", cityId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to delete City with Id: {CityId}. No changes were detected.", cityId);
            return Result<bool>.Failure(CityErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete City with Id: {CityId}",
                             nameof(CityService), cityId);
            return Result<bool>.Failure(CityErrors.DeletionUnexpectedError);
        }
    }
}
