using AutoMapper;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PetService> _logger;
    private readonly IMapper _mapper;

    public PetService(IPetRepository petRepository, IUnitOfWork unitOfWork, ILogger<PetService> logger, IMapper mapper)
    {
        _petRepository = petRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<bool>> CreatePetAsync(PetCreateRequest petCreateRequest, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating a Pet with Id: {PetId} from UserId: {UserId}", petCreateRequest.Id, petCreateRequest.PostedByUserId);

            var pet = _mapper.Map<Pet>(petCreateRequest);
            await _petRepository.InsertAsync(pet, cancellationToken);
            var petCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (petCreated)
            {
                _logger.LogInformation("Successfully created a Pet with Id: {PetId} from UserId: {UserId}", petCreateRequest.Id, petCreateRequest.PostedByUserId);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to create a Pet with Id: {PetId} from UserId: {UserId}", petCreateRequest.Id, petCreateRequest.PostedByUserId);
            return Result<bool>.Failure(PetErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Pet for the UserId: {UserId}",
                             nameof(PetService), petCreateRequest.PostedByUserId);
            return Result<bool>.Failure(PetErrors.CreationUnexpectedError);
        }
    }

    /// <summary>
    /// Retrieves all pets from the system.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing a collection of all pets.</returns>
    public async Task<Result<IEnumerable<Pet>>> GetAllPetsAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all pets.");

            var pets = await _petRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved all pets.");
            return Result<IEnumerable<Pet>>.Success(pets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all pets.",
                             nameof(PetService));
            return Result<IEnumerable<Pet>>.Failure(PetErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Retrieves a pet by its unique ID.
    /// </summary>
    /// <param name="petId">The ID of the pet to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result containing the pet entity if found, or an error if not found.</returns>
    public async Task<Result<Pet>> GetPetByIdAsync(int petId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Pet with Id: {PetId}", petId);

            var pet = await _petRepository.GetByIdAsync(petId, cancellationToken);

            if (pet is null)
            {
                _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                return Result<Pet>.Failure(PetErrors.NotFound(petId));
            }

            _logger.LogInformation("Successfully retrieved Pet with Id: {PetId}", petId);
            return Result<Pet>.Success(pet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Pet with Id: {PetId}",
                             nameof(PetService), petId);
            return Result<Pet>.Failure(PetErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Updates an existing pet in the system.
    /// </summary>
    /// <param name="pet">The pet entity to update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the update was successful.</returns>
    public async Task<Result<bool>> UpdatePetAsync(Pet pet, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);

            await _petRepository.UpdateAsync(pet, cancellationToken);
            var petUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (petUpdated)
            {
                _logger.LogInformation("Successfully updated Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to update Pet with Id: {PetId} from UserId: {UserId}. No changes were detected.", pet.Id, pet.PostedByUserId);
            return Result<bool>.Failure(PetErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Pet with Id: {PetId} for the UserId: {UserId}",
                             nameof(PetService), pet.Id, pet.PostedByUserId);
            return Result<bool>.Failure(PetErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Deletes a pet by its unique ID.
    /// </summary>
    /// <param name="petId">The ID of the pet to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the deletion was successful.</returns>
    public async Task<Result<bool>> DeletePetAsync(int petId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting Pet with Id: {PetId}", petId);

            var pet = await _petRepository.GetByIdAsync(petId, cancellationToken);
            if (pet is null)
            {
                _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                return Result<bool>.Failure(PetErrors.NotFound(petId));
            }

            await _petRepository.DeleteAsync(pet, cancellationToken);
            var petDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (petDeleted)
            {
                _logger.LogInformation("Successfully deleted Pet with Id: {PetId}", petId);
                return Result<bool>.Success(true);
            }
            _logger.LogWarning("Failed to delete Pet with Id: {PetId}. No changes were detected.", petId);
            return Result<bool>.Failure(PetErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Pet with Id: {PetId}",
                             nameof(PetService), petId);
            return Result<bool>.Failure(PetErrors.DeletionUnexpectedError);
        }
    }
}
