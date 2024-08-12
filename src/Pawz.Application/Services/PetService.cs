using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pawz.Domain.Common;

namespace Pawz.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PetService> _logger;

        public PetService(IPetRepository petRepository, IUnitOfWork unitOfWork, ILogger<PetService> logger)
        {
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<bool>> CreatePetAsync(Pet pet, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started creating a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);

                await _petRepository.InsertAsync(pet, cancellationToken);
                var petCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

                if (petCreated)
                {
                    _logger.LogInformation("Successfully created a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
                    return Result<bool>.Success(true);
                }
                _logger.LogWarning("Failed to create a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
                return Result<bool>.Failure(PetErrors.CreationFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Pet for the UserId: {UserId}",
                                 nameof(PetService), pet.PostedByUserId);
                return Result<bool>.Failure(PetErrors.CreationUnexpectedError);
            }
        }

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
}
