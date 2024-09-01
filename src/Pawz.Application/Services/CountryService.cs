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

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CountryService> _logger;

    public CountryService(ICountryRepository countryRepository, IUnitOfWork unitOfWork, ILogger<CountryService> logger)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new country and saves it to the database.
    /// </summary>
    /// <param name="country">The country to create.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A result indicating success or failure.</returns>
    public async Task<Result<bool>> CreateCountryAsync(Country country, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating a Country with Id: {CountryId}", country.Id);

            await _countryRepository.InsertAsync(country, cancellationToken);
            var countryCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (countryCreated)
            {
                _logger.LogInformation("Successfully created a Country with Id: {CountryId}", country.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to create a Country with Id: {CountryId}", country.Id);
            return Result<bool>.Failure(CountryErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Country with Id: {CountryId}",
                             nameof(CountryService), country.Id);
            return Result<bool>.Failure(CountryErrors.CreationUnexpectedError);
        }
    }

    /// <summary>
    /// Retrieves all countries from the database.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A result containing a list of countries.</returns>
    public async Task<Result<List<Country>>> GetAllCountriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all countries.");

            var countries = await _countryRepository.GetAllAsync(cancellationToken);

            if (countries is null)
            {
                _logger.LogError("No countries found");
                return Result<List<Country>>.Failure(CountryErrors.RetrievalError);
            }

            _logger.LogInformation("Successfully retrieved all countries.");
            return Result<List<Country>>.Success(countries.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all countries.", nameof(CountryService));
            return Result<List<Country>>.Failure(CountryErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Retrieves a country by its Id.
    /// </summary>
    /// <param name="countryId">The Id of the country to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A result containing the country if found.</returns>
    public async Task<Result<Country>> GetCountryByIdAsync(int countryId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Country with Id: {CountryId}", countryId);

            var country = await _countryRepository.GetByIdAsync(countryId, cancellationToken);

            if (country is null)
            {
                _logger.LogWarning("Country with Id: {CountryId} was not found.", countryId);
                return Result<Country>.Failure(CountryErrors.NotFound(countryId));
            }

            _logger.LogInformation("Successfully retrieved Country with Id: {CountryId}", countryId);
            return Result<Country>.Success(country);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Country with Id: {CountryId}",
                             nameof(CountryService), countryId);
            return Result<Country>.Failure(CountryErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Updates an existing country in the database.
    /// </summary>
    /// <param name="country">The country to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A result indicating success or failure.</returns>
    public async Task<Result<bool>> UpdateCountryAsync(Country country, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating Country with Id: {CountryId}", country.Id);

            await _countryRepository.UpdateAsync(country, cancellationToken);
            var countryUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (countryUpdated)
            {
                _logger.LogInformation("Successfully updated Country with Id: {CountryId}", country.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to update Country with Id: {CountryId}. No changes were detected.", country.Id);
            return Result<bool>.Failure(CountryErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Country with Id: {CountryId}",
                             nameof(CountryService), country.Id);
            return Result<bool>.Failure(CountryErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a country from the database by its Id.
    /// </summary>
    /// <param name="countryId">The Id of the country to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A result indicating success or failure.</returns>
    public async Task<Result<bool>> DeleteCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting Country with Id: {CountryId}", countryId);

            var country = await _countryRepository.GetByIdAsync(countryId, cancellationToken);
            if (country is null)
            {
                _logger.LogWarning("Country with Id: {CountryId} was not found.", countryId);
                return Result<bool>.Failure(CountryErrors.NotFound(countryId));
            }

            await _countryRepository.DeleteAsync(country, cancellationToken);
            var countryDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (countryDeleted)
            {
                _logger.LogInformation("Successfully deleted Country with Id: {CountryId}", countryId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to delete Country with Id: {CountryId}. No changes were detected.", countryId);
            return Result<bool>.Failure(CountryErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Country with Id: {CountryId}",
                             nameof(CountryService), countryId);
            return Result<bool>.Failure(CountryErrors.DeletionUnexpectedError);
        }
    }
}
