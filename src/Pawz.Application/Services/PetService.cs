using AutoMapper;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.PetModels;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PetService> _logger;
    private readonly IFileUploaderService _fileUploaderService;
    private readonly IPetImageRepository _petImageRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;
    private readonly ILocationService _locationService;

    public PetService(IPetRepository petRepository,
                      IUnitOfWork unitOfWork,
                      ILogger<PetService> logger,
                      IFileUploaderService fileUploaderService,
                      IPetImageRepository petImageRepository,
                      IUserAccessor userAccessor,
                      IMapper mapper,
                      ILocationService locationService)
    {
        _petRepository = petRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileUploaderService = fileUploaderService;
        _petImageRepository = petImageRepository;
        _userAccessor = userAccessor;
        _mapper = mapper;
        _locationService = locationService;
    }

    /// <summary>
    /// Creates a new pet in the system.
    /// </summary>
    /// <param name="pet">The pet entity to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the creation was successful.</returns>
    public async Task<Result<int>> CreatePetAsync(PetCreateRequest petCreateRequest, CancellationToken cancellationToken)
    {
        try
        {
            petCreateRequest.PostedByUserId = _userAccessor.GetUserId();
            _logger.LogInformation("Started creating a Pet with Id: {PetId} from UserId: {UserId}", petCreateRequest.Id, petCreateRequest.PostedByUserId);

            var location = new Location
            {
                CityId = petCreateRequest.CityId,
                Address = petCreateRequest.Address,
                PostalCode = petCreateRequest.PostalCode
            };

            var locationInsertResult = await _locationService.CreateLocationAsync(location, cancellationToken);

            if (!locationInsertResult.IsSuccess)
            {
                _logger.LogError("Failed to create a location for the pet with Id: {PetId}", petCreateRequest.Id);
                return Result<int>.Failure(LocationErrors.CreationFailed);
            }

            var pet = _mapper.Map<Pet>(petCreateRequest);
            pet.Location = locationInsertResult.Value;
            pet.PostedByUserId = _userAccessor.GetUserId();
            pet.Status = PetStatus.Available;
            pet.CreatedAt = DateTime.UtcNow;

            var petId = await _petRepository.InsertAsync(pet, cancellationToken);
            var petCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (petCreated is false)
            {
                _logger.LogError("Failed to create a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
                return Result<int>.Failure(PetErrors.CreationFailed);
            }

            var imageFilesList = petCreateRequest.ImageFiles.ToList();

            for (int i = 0; i < imageFilesList.Count; i++)
            {
                var imageFile = imageFilesList[i];

                var uploadedFileName = await _fileUploaderService.UploadFileAsync(imageFile);
                var fileName = uploadedFileName.Value;

                var petImage = new PetImage
                {
                    PetId = pet.Id,
                    ImageUrl = fileName,
                    IsPrimary = i == 0 ? true : false,
                    UploadedAt = DateTime.UtcNow,
                };

                await _petImageRepository.InsertAsync(petImage, cancellationToken);
            }

            var arePetImagesSaved = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (arePetImagesSaved is false)
            {
                _logger.LogError("Failed to save pet images; the pet creation process cannot be completed.");
                return Result<int>.Failure(PetErrors.CreationFailed);
            }

            _logger.LogInformation("Successfully created a Pet with Id: {PetId} from UserId: {UserId}", pet.Id, pet.PostedByUserId);
            return Result<int>.Success(pet.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {ServiceName} while attempting to create a Pet for UserId: {UserId}", nameof(PetService), petCreateRequest.PostedByUserId);
            return Result<int>.Failure(PetErrors.CreationUnexpectedError);
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
    public async Task<Result<PetResponse>> GetPetByIdAsync(int petId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Pet with Id: {PetId}", petId);

            var pet = await _petRepository.GetPetByIdWithRelatedEntitiesAsync(petId, cancellationToken);

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
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Pet with Id: {PetId}",
                             nameof(PetService), petId);
            return Result<PetResponse>.Failure(PetErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Deletes a pet by its unique ID.
    /// </summary>
    /// <param name="petId">The ID of the pet to delete.</param
    /// <param name="userId">The ID of the user who created the pet.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A result indicating whether the deletion was successful.</returns>
    public async Task<Result<bool>> DeletePetAsync(int petId, string userId, CancellationToken cancellationToken)
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

            if (pet.PostedByUserId != userId)
            {
                _logger.LogWarning("User {UserId} is not authorized to delete Pet with Id: {PetId}", userId, petId);
                return Result<bool>.Failure(PetErrors.Unauthorized);
            }

            pet.IsDeleted = true;
            pet.DeletedAt = DateTimeOffset.UtcNow;

            await _petRepository.UpdateAsync(pet, cancellationToken);
            var petDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (petDeleted)
            {
                _logger.LogInformation("Successfully soft-deleted Pet with Id: {PetId}", petId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to soft-delete Pet with Id: {PetId}. No changes were detected.", petId);
            return Result<bool>.Failure(PetErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Pet with Id: {PetId}",
                             nameof(PetService), petId);
            return Result<bool>.Failure(PetErrors.DeletionUnexpectedError);
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

    public async Task<Result<bool>> UpdatePetAsync(Pet pet, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _userAccessor.GetUserId();
            _logger.LogInformation("Started updating Pet with Id: {PetId} from UserId: {UserId}", pet.Id, userId);

            var existingPet = await _petRepository.GetPetByIdWithRelatedEntitiesAsync(pet.Id, cancellationToken);
            if (existingPet == null)
            {
                _logger.LogWarning("Pet with Id: {PetId} not found", pet.Id);
                return Result<bool>.Failure(PetErrors.NoPetsFound());
            }

            if (existingPet.PostedByUserId != userId)
            {
                _logger.LogWarning("UserId: {UserId} is not authorized to update Pet with Id: {PetId}", userId, pet.Id);
                return Result<bool>.Failure(PetErrors.UpdateUnexpectedError);
            }

            existingPet.Name = pet.Name;
            existingPet.AgeYears = pet.AgeYears;
            existingPet.About = pet.About;
            existingPet.Price = pet.Price;
            existingPet.BreedId = pet.BreedId;

            var petUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (petUpdated)
            {
                _logger.LogInformation("Successfully updated Pet with Id: {PetId} from UserId: {UserId}", pet.Id, userId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to update Pet with Id: {PetId} from UserId: {UserId}. No changes were detected.", pet.Id, userId);
            return Result<bool>.Failure(PetErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Pet with Id: {PetId} for UserId: {UserId}",
                             nameof(PetService), pet.Id, _userAccessor.GetUserId());
            return Result<bool>.Failure(PetErrors.UpdateUnexpectedError);
        }
    }

    /// <summary>
    /// Asynchronously retrieves a paginated list of pets for the current user using cursor-based pagination.
    /// </summary>
    /// <param name="pageSize">The maximum number of pets to return per page.</param>
    /// <param name="cursor">The cursor (pet ID) from which to start the next set of pets, or null for the first page.</param>
    /// <param name="cancellationToken">A cancellation token to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> 
    /// with a tuple of a collection of <see cref="UserPetResponse"/> and the next cursor string.
    /// </returns>
    public async Task<Result<(IEnumerable<UserPetResponse> Pets, string NextCursor)>> GetPetsByUserIdWithCursorPaginationAsync(
        int pageSize, string cursor, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _userAccessor.GetUserId();
            _logger.LogInformation("Started retrieving pets for UserId: {UserId} with cursor pagination", userId);

            var (pets, nextCursor) = await _petRepository.GetPetsByUserIdWithCursorPaginationAsync(
                userId, pageSize, cursor, cancellationToken);

            var petResponses = _mapper.Map<IEnumerable<UserPetResponse>>(pets);

            _logger.LogInformation("Successfully retrieved pets for UserId: {UserId} with cursor pagination", userId);
            return Result<(IEnumerable<UserPetResponse>, string)>.Success((petResponses, nextCursor));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving pets for UserId: {UserId} with cursor pagination",
                             _userAccessor.GetUserId());
            return Result<(IEnumerable<UserPetResponse>, string)>.Failure(PetErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Asynchronously retrieves the total count of pets for the current user.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> 
    /// indicating the total number of pets posted by the user.
    /// </returns>
    public async Task<Result<int>> GetTotalPetsCountForUserAsync(CancellationToken cancellationToken)
    {
        try
        {
            var userId = _userAccessor.GetUserId();
            _logger.LogInformation("Getting total pet count for user: {UserId}", userId);

            var count = await _petRepository.GetTotalPetsCountForUserAsync(userId, cancellationToken);

            _logger.LogInformation("Total pet count for user {UserId}: {Count}", userId, count);
            return Result<int>.Success(count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting total pet count for user");
            return Result<int>.Failure("Failed to get total pet count");
        }
    }
}
