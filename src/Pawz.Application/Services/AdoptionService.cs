using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pawz.Domain.Common;

namespace Pawz.Application.Services;

public class AdoptionService : IAdoptionService
{
    private readonly IAdoptionRepository _adoptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdoptionService> _logger;

    public AdoptionService(IAdoptionRepository adoptionRepository, IUnitOfWork unitOfWork, ILogger<AdoptionService> logger)
    {
        _adoptionRepository = adoptionRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new adoption record.
    /// </summary>
    /// <param name="adoption">The adoption entity to be created.</param>
    /// <param name="cancellationToken">A cancellation token for async operations.</param>
    /// <returns>A <see cref="Result{T}"/> indicating the success or failure of the operation.</returns>
    public async Task<Result<bool>> CreateAdoptionAsync(Adoption adoption, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating an Adoption with Id: {AdoptionId} from UserId: {UserId}", adoption.Id, adoption.AdoptionRequestId);

            await _adoptionRepository.InsertAsync(adoption, cancellationToken);
            var isAdoptionCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isAdoptionCreated)
            {
                _logger.LogInformation("Created an Adoption with Id: {AdoptionId} for UserId: {UserId}", adoption.Id, adoption.AdoptionRequestId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to create an Adoption with Id: {AdoptionId} from UserId: {UserId}", adoption.Id, adoption.AdoptionRequestId);
            return Result<bool>.Failure(AdoptionErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Pet for the UserId: {UserId}",
                             nameof(AdoptionService), adoption.AdoptionRequestId);
            return Result<bool>.Failure(AdoptionErrors.CreationUnexpectedError);
        }
    }

    /// <summary>
    /// Retrieves all adoption records.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for async operations.</param>
    /// <returns>A <see cref="Result{T}"/> containing a collection of adoptions.</returns>
    public async Task<Result<IEnumerable<Adoption>>> GetAllAdoptionsAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all adoptions.");

            var adoptions = await _adoptionRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved all adoptions.");
            return Result<IEnumerable<Adoption>>.Success(adoptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all adoptions.",
                             nameof(AdoptionService));
            return Result<IEnumerable<Adoption>>.Failure(AdoptionErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Retrieves a specific adoption record by its identifier.
    /// </summary>
    /// <param name="adoptionId">The identifier of the adoption to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token for async operations.</param>
    /// <returns>A <see cref="Result{T}"/> containing the adoption entity, or a failure result if not found.</returns>
    public async Task<Result<Adoption>> GetAdoptionByIdAsync(int adoptionId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Adoptions with Id: {AdoptionId}", adoptionId);

            var adoption = await _adoptionRepository.GetByIdAsync(adoptionId, cancellationToken);

            if (adoption is null)
            {
                _logger.LogWarning("Adoption with Id: {AdoptionId} was not found.", adoptionId);
                return Result<Adoption>.Failure(AdoptionErrors.NotFound(adoptionId));
            }

            _logger.LogInformation("Successfully retrieved Adoption with Id: {AdoptionId}", adoptionId);
            return Result<Adoption>.Success(adoption);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Adoption with Id: {AdoptionId}",
                             nameof(AdoptionService), adoptionId);
            return Result<Adoption>.Failure(AdoptionErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Updates an existing adoption record.
    /// </summary>
    /// <param name="adoption">The adoption entity with updated information.</param>
    /// <param name="cancellationToken">A cancellation token for async operations.</param>
    /// <returns>A <see cref="Result{T}"/> indicating the success or failure of the operation.</returns>
    public async Task<Result<bool>> UpdateAdoptionAsync(Adoption adoption, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating Adoption with Id: {AdoptionId} from UserId: {UserId}", adoption.Id, adoption.AdoptionRequestId);

            await _adoptionRepository.UpdateAsync(adoption, cancellationToken);
            var adoptionUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (adoptionUpdated)
            {
                _logger.LogInformation("Successfully updated Adoption with Id: {AdoptionId} from UserId: {UserId}", adoption.Id, adoption.AdoptionRequestId);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to update Adoption with Id: {AdoptionId} from UserId: {UserId}. No changes were detected.", adoption.Id, adoption.AdoptionRequestId);
            return Result<bool>.Failure(AdoptionErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Adoption with Id: {AdoptionId} for the UserId: {UserId}",
                             nameof(AdoptionService), adoption.Id, adoption.AdoptionRequestId);
            return Result<bool>.Failure(AdoptionErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a specific adoption record by its identifier.
    /// </summary>
    /// <param name="adoptionId">The identifier of the adoption to delete.</param>
    /// <param name="cancellationToken">A cancellation token for async operations.</param>
    /// <returns>A <see cref="Result{T}"/> indicating the success or failure of the operation.</returns>
    public async Task<Result<bool>> DeleteAdoptionAsync(int adoptionId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting Adoption with Id: {AdoptionId}", adoptionId);

            var adoption = await _adoptionRepository.GetByIdAsync(adoptionId, cancellationToken);
            if (adoption is null)
            {
                _logger.LogWarning("Adoption with Id: {AdoptionId} was not found.", adoptionId);
                return Result<bool>.Failure(AdoptionErrors.NotFound(adoptionId));
            }

            await _adoptionRepository.DeleteAsync(adoption, cancellationToken);
            var adoptionDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (adoptionDeleted)
            {
                _logger.LogInformation("Successfully deleted Adoption with Id: {AdoptionId}", adoptionId);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to delete Adoption with Id: {AdoptionId}. No changes were detected.", adoptionId);
            return Result<bool>.Failure(AdoptionErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Adoption with Id: {AdoptionId}",
                             nameof(AdoptionService), adoptionId);
            return Result<bool>.Failure(AdoptionErrors.DeletionUnexpectedError);
        }
    }
}
