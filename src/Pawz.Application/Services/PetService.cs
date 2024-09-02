using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pawz.Domain.Common;
using AutoMapper;
using Pawz.Application.Models.PetModels;
using Pawz.Application.Models.Pet;
using System.Linq;

namespace Pawz.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PetService> _logger;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public PetService(IPetRepository petRepository, IUnitOfWork unitOfWork, ILogger<PetService> logger, IUserAccessor userAccessor, IMapper mapper)
        {
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }

        public async Task<Result<PetResponse>> CreatePetAsync(PetRequest petRequest, CancellationToken cancellationToken)
        {
            try
            {
                var pet = _mapper.Map<Pet>(petRequest);

                _logger.LogInformation("Started creating a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);

                await _petRepository.InsertAsync(pet, cancellationToken);
                var petCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

                if (petCreated)
                {
                    _logger.LogInformation("Successfully created a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);

                    var petResponse = _mapper.Map<PetResponse>(pet);

                    return Result<PetResponse>.Success(petResponse);
                }

                _logger.LogWarning("Failed to create a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
                return Result<PetResponse>.Failure(PetErrors.CreationFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Pet for the UserId: {UserId}", nameof(PetService), petRequest.PostedByUserId);
                return Result<PetResponse>.Failure(PetErrors.CreationUnexpectedError);
            }
        }

        public async Task<Result<IEnumerable<PetResponse>>> GetAllPetsAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started retrieving all pets.");

                var pets = await _petRepository.GetAllAsync(cancellationToken);

                var petResponses = _mapper.Map<IEnumerable<PetResponse>>(pets);

                _logger.LogInformation("Successfully retrieved all pets.");
                return Result<IEnumerable<PetResponse>>.Success(petResponses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all pets.", nameof(PetService));
                return Result<IEnumerable<PetResponse>>.Failure(PetErrors.RetrievalError);
            }
        }

        public async Task<Result<PetResponse>> GetPetByIdAsync(int petId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started retrieving Pet with Id: {PetId}", petId);

                var pet = await _petRepository.GetByIdAsync(petId, cancellationToken);

                if (pet is null)
                {
                    _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                    return Result<PetResponse>.Failure(PetErrors.NotFound(petId));
                }

                var petResponse = _mapper.Map<PetResponse>(pet);

                _logger.LogInformation("Successfully retrieved Pet with Id: {PetId}", petId);
                return Result<PetResponse>.Success(petResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Pet with Id: {PetId}", nameof(PetService), petId);
                return Result<PetResponse>.Failure(PetErrors.RetrievalError);
            }
        }

        public async Task<Result<PetResponse>> UpdatePetAsync(int petId, PetRequest petRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started updating Pet with Id: {PetId}", petId);

                var existingPet = await _petRepository.GetByIdAsync(petId, cancellationToken);
                if (existingPet is null)
                {
                    _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                    return Result<PetResponse>.Failure(PetErrors.NotFound(petId));
                }

                _mapper.Map(petRequest, existingPet);

                await _petRepository.UpdateAsync(existingPet, cancellationToken);
                var petUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

                if (petUpdated)
                {
                    var petResponse = _mapper.Map<PetResponse>(existingPet);
                    _logger.LogInformation("Successfully updated Pet with Id: {PetId}", petId);
                    return Result<PetResponse>.Success(petResponse);
                }

                _logger.LogWarning("Failed to update Pet with Id: {PetId}. No changes were detected.", petId);
                return Result<PetResponse>.Failure(PetErrors.NoChangesDetected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Pet with Id: {PetId}", nameof(PetService), petId);
                return Result<PetResponse>.Failure(PetErrors.UpdateUnexpectedError);
            }
        }

        public async Task<Result<PetResponse>> DeletePetAsync(int petId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started deleting Pet with Id: {PetId}", petId);

                var pet = await _petRepository.GetByIdAsync(petId, cancellationToken);
                if (pet is null)
                {
                    _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                    return Result<PetResponse>.Failure(PetErrors.NotFound(petId));
                }

                var petResponse = _mapper.Map<PetResponse>(pet);

                await _petRepository.DeleteAsync(pet, cancellationToken);
                var petDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

                if (petDeleted)
                {
                    _logger.LogInformation("Successfully deleted Pet with Id: {PetId}", petId);
                    return Result<PetResponse>.Success(petResponse);
                }

                _logger.LogWarning("Failed to delete Pet with Id: {PetId}. No changes were detected.", petId);
                return Result<PetResponse>.Failure(PetErrors.NoChangesDetected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Pet with Id: {PetId}", nameof(PetService), petId);
                return Result<PetResponse>.Failure(PetErrors.DeletionUnexpectedError);
            }
        }

        public async Task<Result<IEnumerable<PetResponse>>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Started retrieving all pets with related entities.");

                var pets = await _petRepository.GetAllPetsWithRelatedEntities(cancellationToken);

                if (pets is null)
                {
                    _logger.LogWarning("No pets were found with related entities.");
                    return Result<IEnumerable<PetResponse>>.Failure(PetErrors.NoPetsFound());
                }

                var petResponses = _mapper.Map<IEnumerable<PetResponse>>(pets);

                _logger.LogInformation("Successfully retrieved all pets with related entities.");
                return Result<IEnumerable<PetResponse>>.Success(petResponses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all pets with related entities.", nameof(PetService));
                return Result<IEnumerable<PetResponse>>.Failure(PetErrors.RetrievalError);
            }
        }

        public async Task<int> CountPetsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Started counting the number of pets in the database.");

                var count = await _petRepository.CountPetsAsync(cancellationToken);

                _logger.LogInformation("Successfully counted the number of pets in the database. Total count: {Count}", count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to count the number of pets.", nameof(PetService));
                throw;
            }
        }

        /// <summary>
        /// Retrieves all pets associated with a specific user by their unique user ID.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A result containing a collection of pets associated with the user, or an error if not found.</returns>
        public async Task<Result<IEnumerable<UserPetResponse>>> GetPetsByUserIdAsync(CancellationToken cancellationToken)
        {
            string userId = null;
            try
            {
                userId = _userAccessor.GetUserId();
                _logger.LogInformation("Started retrieving pets for UserId: {UserId}", userId);

                var pets = await _petRepository.GetByUserIdAsync(userId, cancellationToken);

                if (pets is null || !pets.Any())
                {
                    _logger.LogWarning("No pets found for UserId: {UserId}", userId);
                    return Result<IEnumerable<UserPetResponse>>.Failure(PetErrors.NoPetsFoundForUser(userId));
                }

                var petResponses = _mapper.Map<IEnumerable<UserPetResponse>>(pets);

                _logger.LogInformation("Successfully retrieved pets for UserId: {UserId}", userId);
                return Result<IEnumerable<UserPetResponse>>.Success(petResponses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve pets for UserId: {UserId}",
                                 nameof(PetService), userId);
                return Result<IEnumerable<UserPetResponse>>.Failure(PetErrors.RetrievalError);
            }
        }
    }
}