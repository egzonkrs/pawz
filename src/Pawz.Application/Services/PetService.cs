using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.PetModels;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using Pawz.Domain.Helpers;
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

            for (var i = 0; i < imageFilesList.Count; i++)
            {
                var imageFile = imageFilesList[i];

                var uploadedFileName = await _fileUploaderService.UploadFileAsync(imageFile);
                var fileName = uploadedFileName.Value;

                var petImage = new PetImage
                {
                    PetId = pet.Id,
                    ImageUrl = fileName,
                    IsPrimary = i is 0,
                    UploadedAt = DateTime.UtcNow,
                    Pet = pet
                };

                await _petImageRepository.InsertAsync(petImage, cancellationToken);
            }

            var arePetImagesSaved = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (arePetImagesSaved is false)
            {
                _logger.LogError("Failed to save pet images; the pet creation process cannot be completed.");
                return Result<int>.Failure(PetErrors.CreationFailed);
            }

            return Result<int>.Success(pet.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {ServiceName} while attempting to create a Pet for UserId: {UserId}", nameof(PetService),
                petCreateRequest.PostedByUserId);
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
            var pets = await _petRepository.GetAllAsync(cancellationToken);

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
            var pet = await _petRepository.GetPetByIdWithRelatedEntitiesAsync(petId, cancellationToken);

            if (pet is null)
            {
                _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                return Result<PetResponse>.Failure(PetErrors.NotFound(petId));
            }

            var petResponse = _mapper.Map<PetResponse>(pet);

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

            var pets = await _petRepository.GetByUserIdAsync(userId, cancellationToken);

            if (pets is null || !pets.Any())
            {
                return Result<IEnumerable<UserPetResponse>>.Failure(PetErrors.NoPetsFoundForUser(userId));
            }

            var petResponses = _mapper.Map<IEnumerable<UserPetResponse>>(pets);

            return Result<IEnumerable<UserPetResponse>>.Success(petResponses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve pets for UserId: {UserId}",
                nameof(PetService), userId);
            return Result<IEnumerable<UserPetResponse>>.Failure(PetErrors.RetrievalError);
        }
    }

    /// <summary>
    /// Asynchronously retrieves all pets along with their related entities.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation,
    /// containing a <see cref="Result{T}"/> of an <see cref="IEnumerable{PetResponse}"/> with pets and their related entities.</returns>
    public async Task<Result<IEnumerable<PetResponse>>> GetAllPetsWithRelatedEntities(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Started retrieving all pets with related entities.");

            var pets = await _petRepository.GetAllPetsWithRelatedEntitiesAsync(cancellationToken);

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

    /// <summary>
    /// Asynchronously retrieves a filtered collection of pets with related entities based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The parameters used for filtering, sorting, and pagination of pets.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation,
    /// containing a <see cref="Result{T}"/> of an <see cref="IEnumerable{PetResponse}"/> with pets and their related entities.</returns>
    public async Task<Result<IEnumerable<PetResponse>>> GetAllPetsWithRelatedEntities(PetQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrEmpty(queryParams.SpeciesName) && string.IsNullOrEmpty(queryParams.BreedName))
            {
                var allPets = await _petRepository.GetAllPetsWithRelatedEntitiesAsync(cancellationToken);

                if (allPets is null || !allPets.Any())
                {
                    return Result<IEnumerable<PetResponse>>.Failure(PetErrors.NoPetsFound());
                }

                var allPetResponses = _mapper.Map<IEnumerable<PetResponse>>(allPets);
                return Result<IEnumerable<PetResponse>>.Success(allPetResponses);
            }

            var filteredPetsQuery = _petRepository.GetAllPetsWithRelatedEntitiesAsQueryable(queryParams);
            var filteredPets = await filteredPetsQuery.ToListAsync(cancellationToken);

            if (filteredPets is null || !filteredPets.Any())
            {
                return Result<IEnumerable<PetResponse>>.Failure(PetErrors.NoPetsFound());
            }

            var filteredPetResponses = _mapper.Map<IEnumerable<PetResponse>>(filteredPets);
            return Result<IEnumerable<PetResponse>>.Success(filteredPetResponses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve pets with related entities.", nameof(PetService));
            return Result<IEnumerable<PetResponse>>.Failure(PetErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> UpdatePetAsync(Pet pet, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _userAccessor.GetUserId();

            var existingPet = await _petRepository.GetPetByIdWithRelatedEntitiesAsync(pet.Id, cancellationToken);
            if (existingPet == null)
            {
                _logger.LogWarning("Pet with Id: {PetId} not found", pet.Id);
                return Result<bool>.Failure(PetErrors.NoPetsFound());
            }

            if (existingPet.PostedByUserId != userId)
            {
                _logger.LogWarning("UserId: {UserId} is not authorized to update Pet with Id: {PetId}", userId, pet.Id);
                return Result<bool>.Failure(UsersErrors.Unauthorized);
            }

            existingPet.Name = pet.Name;
            existingPet.AgeYears = pet.AgeYears;
            existingPet.About = pet.About;
            existingPet.Price = pet.Price;
            existingPet.BreedId = pet.BreedId;

            var petUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (petUpdated)
            {
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
}
