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

public class SpeciesService : ISpeciesService
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SpeciesService> _logger;

    public SpeciesService(ISpeciesRepository speciesRepository, IUnitOfWork unitOfWork, ILogger<SpeciesService> logger)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new species in the system.
    /// </summary>
    /// <param name="species">The species entity to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the creation was successful.</returns>
    public async Task<Result<bool>> CreateSpeciesAsync(Species species, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating a Species with Id: {SpeciesId}", species.Id);

            await _speciesRepository.InsertAsync(species, cancellationToken);
            var speciesCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (speciesCreated)
            {
                _logger.LogInformation("Successfully created a Species with Id: {SpeciesId}", species.Id);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to create a Species with Id: {SpeciesId}", species.Id);
            return Result<bool>.Failure(SpeciesErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Species with Id: {SpeciesId}",
                             nameof(SpeciesService), species.Id);
            return Result<bool>.Failure(SpeciesErrors.CreationUnexpectedError);
        }
    }

    /// <summary>
    /// Retrieves all species from the system.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing a collection of all species.</returns>
    public async Task<Result<IEnumerable<Species>>> GetAllSpeciesAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all species.");

            var species = await _speciesRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved all species.");
            return Result<IEnumerable<Species>>.Success(species);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all species.",
                             nameof(SpeciesService));
            return Result<IEnumerable<Species>>.Failure(SpeciesErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Retrieves a species by its unique ID.
    /// </summary>
    /// <param name="speciesId">The ID of the species to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the species entity if found, or an error if not found.</returns>
    public async Task<Result<Species>> GetSpeciesByIdAsync(int speciesId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Species with Id: {SpeciesId}", speciesId);

            var species = await _speciesRepository.GetByIdAsync(speciesId, cancellationToken);

            if (species is null)
            {
                _logger.LogWarning("Species with Id: {SpeciesId} was not found.", speciesId);
                return Result<Species>.Failure(SpeciesErrors.NotFound(speciesId));
            }

            _logger.LogInformation("Successfully retrieved Species with Id: {SpeciesId}", speciesId);
            return Result<Species>.Success(species);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Species with Id: {SpeciesId}",
                             nameof(SpeciesService), speciesId);
            return Result<Species>.Failure(SpeciesErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Updates an existing species in the system.
    /// </summary>
    /// <param name="species">The species entity to update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the update was successful.</returns>
    public async Task<Result<bool>> UpdateSpeciesAsync(Species species, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating Species with Id: {SpeciesId}", species.Id);

            await _speciesRepository.UpdateAsync(species, cancellationToken);
            var speciesUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (speciesUpdated)
            {
                _logger.LogInformation("Successfully updated Species with Id: {SpeciesId}", species.Id);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to update Species with Id: {SpeciesId}. No changes were detected.", species.Id);
            return Result<bool>.Failure(SpeciesErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Species with Id: {SpeciesId}",
                             nameof(SpeciesService), species.Id);
            return Result<bool>.Failure(SpeciesErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a species by its unique ID.
    /// </summary>
    /// <param name="speciesId">The ID of the species to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the deletion was successful.</returns>
    public async Task<Result<bool>> DeleteSpeciesAsync(int speciesId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting Species with Id: {SpeciesId}", speciesId);

            var species = await _speciesRepository.GetByIdAsync(speciesId, cancellationToken);
            if (species is null)
            {
                _logger.LogWarning("Species with Id: {SpeciesId} was not found.", speciesId);
                return Result<bool>.Failure(SpeciesErrors.NotFound(speciesId));
            }

            await _speciesRepository.DeleteAsync(species, cancellationToken);
            var speciesDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (speciesDeleted)
            {
                _logger.LogInformation("Successfully deleted Species with Id: {SpeciesId}", speciesId);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to delete Species with Id: {SpeciesId}. No changes were detected.", speciesId);
            return Result<bool>.Failure(SpeciesErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Species with Id: {SpeciesId}",
                             nameof(SpeciesService), speciesId);
            return Result<bool>.Failure(SpeciesErrors.DeletionUnexpectedError);
        }
    }
}
