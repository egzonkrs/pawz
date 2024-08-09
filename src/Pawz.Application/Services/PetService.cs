using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<bool> CreatePetAsync(Pet pet, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started creating a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);

                await _petRepository.AddAsync(pet, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully created a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Pet for the UserId: {UserId}",
                                nameof(PetService), pet.PostedByUserId);
                return false;
            }
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started retrieving all pets.");

                var pets = await _petRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved all pets.");
                return pets;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all pets.",
                                nameof(PetService));
                return null;
            }
        }

        public async Task<Pet> GetPetByIdAsync(int petId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started retrieving Pet with Id: {PetId}", petId);

                var pet = await _petRepository.GetByIdAsync(petId, cancellationToken);

                if (pet != null)
                {
                    _logger.LogInformation("Successfully retrieved Pet with Id: {PetId}", petId);
                }
                else
                {
                    _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                }

                return pet;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Pet with Id: {PetId}",
                                nameof(PetService), petId);
                return null;
            }
        }

        public async Task<bool> UpdatePetAsync(Pet pet, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started updating Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);

                await _petRepository.UpdateAsync(pet, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully updated Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Pet with Id: {PetId} for the UserId: {UserId}",
                                nameof(PetService), pet.Id, pet.PostedByUserId);
                return false;
            }
        }

        public async Task<bool> DeletePetAsync(int petId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started deleting Pet with Id: {PetId}", petId);

                var pet = await _petRepository.GetByIdAsync(petId, cancellationToken);
                if (pet != null)
                {
                    await _petRepository.DeleteAsync(pet, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Successfully deleted Pet with Id: {PetId}", petId);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Pet with Id: {PetId}",
                                nameof(PetService), petId);
                return false;
            }
        }
    }
}
