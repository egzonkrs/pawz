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

public class BreedService : IBreedService
{
    private readonly IBreedRepository _breedRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BreedService> _logger;

    public BreedService(IBreedRepository breedRepository, IUnitOfWork unitOfWork, ILogger<BreedService> logger)
    {
        _breedRepository = breedRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new breed entity.
    /// </summary>
    /// <param name="breed">The breed entity to create.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A result indicating success or failure of the operation.</returns>
    public async Task<Result<bool>> CreateBreedAsync(Breed breed, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating a Breed with Id: {BreedId}", breed.Id);

            await _breedRepository.InsertAsync(breed, cancellationToken);
            var breedCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (breedCreated)
            {
                _logger.LogInformation("Successfully created a Breed with Id: {BreedId}", breed.Id);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to create a Breed with Id: {BreedId}", breed.Id);
            return Result<bool>.Failure(BreedErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Breed with Id: {BreedId}",
                             nameof(BreedService), breed.Id);
            return Result<bool>.Failure(BreedErrors.CreationUnexpectedError);
        }
    }

    /// <summary>
    /// Retrieves all breed entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A result containing a collection of breed entities.</returns>
    public async Task<Result<IEnumerable<Breed>>> GetAllBreedsAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all breeds.");

            var breeds = await _breedRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved all breeds.");
            return Result<IEnumerable<Breed>>.Success(breeds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all breeds.",
                             nameof(BreedService));
            return Result<IEnumerable<Breed>>.Failure(BreedErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Retrieves a breed entity by its identifier.
    /// </summary>
    /// <param name="breedId">The identifier of the breed to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A result containing the breed entity if found, otherwise an error.</returns>
    public async Task<Result<Breed>> GetBreedByIdAsync(int breedId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Breed with Id: {BreedId}", breedId);

            var breed = await _breedRepository.GetByIdAsync(breedId, cancellationToken);

            if (breed is null)
            {
                _logger.LogWarning("Breed with Id: {BreedId} was not found.", breedId);
                return Result<Breed>.Failure(BreedErrors.NotFound(breedId));
            }

            _logger.LogInformation("Successfully retrieved Breed with Id: {BreedId}", breedId);
            return Result<Breed>.Success(breed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Breed with Id: {BreedId}",
                             nameof(BreedService), breedId);
            return Result<Breed>.Failure(BreedErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Updates an existing breed entity.
    /// </summary>
    /// <param name="breed">The breed entity to update.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A result indicating success or failure of the update operation.</returns>
    public async Task<Result<bool>> UpdateBreedAsync(Breed breed, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating Breed with Id: {BreedId}", breed.Id);

            await _breedRepository.UpdateAsync(breed, cancellationToken);
            var breedUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (breedUpdated)
            {
                _logger.LogInformation("Successfully updated Breed with Id: {BreedId}", breed.Id);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to update Breed with Id: {BreedId}. No changes were detected.", breed.Id);
            return Result<bool>.Failure(BreedErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Breed with Id: {BreedId}",
                             nameof(BreedService), breed.Id);
            return Result<bool>.Failure(BreedErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a breed entity by its identifier.
    /// </summary>
    /// <param name="breedId">The identifier of the breed to delete.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A result indicating success or failure of the delete operation.</returns>
    public async Task<Result<bool>> DeleteBreedAsync(int breedId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting Breed with Id: {BreedId}", breedId);

            var breed = await _breedRepository.GetByIdAsync(breedId, cancellationToken);
            if (breed is null)
            {
                _logger.LogWarning("Breed with Id: {BreedId} was not found.", breedId);
                return Result<bool>.Failure(BreedErrors.NotFound(breedId));
            }

            await _breedRepository.DeleteAsync(breed, cancellationToken);
            var breedDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (breedDeleted)
            {
                _logger.LogInformation("Successfully deleted Breed with Id: {BreedId}", breedId);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to delete Breed with Id: {BreedId}. No changes were detected.", breedId);
            return Result<bool>.Failure(BreedErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Breed with Id: {BreedId}",
                             nameof(BreedService), breedId);
            return Result<bool>.Failure(BreedErrors.DeletionUnexpectedError);
        }
    }
}

