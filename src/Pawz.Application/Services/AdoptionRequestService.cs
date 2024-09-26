using AutoMapper;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
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

public class AdoptionRequestService : IAdoptionRequestService
{
    private readonly IAdoptionRequestRepository _adoptionRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdoptionService> _logger;
    private readonly IMapper _mapper;
    private readonly ILocationService _locationService;
    private readonly IUserAccessor _userAccessor;

    public AdoptionRequestService(
        IAdoptionRequestRepository adoptionRequestRepository,
        IUnitOfWork unitOfWork,
        ILogger<AdoptionService> logger,
        IMapper mapper,
        ILocationService locationService,
        IUserAccessor userAccessor)
    {
        _adoptionRequestRepository = adoptionRequestRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _locationService = locationService;
        _userAccessor = userAccessor;
    }

    public async Task<Result<bool>> CreateAdoptionRequestAsync(AdoptionRequestCreateRequest adoptionRequestCreateRequest, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started creating an Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequestCreateRequest.Id,
                adoptionRequestCreateRequest);

            var location = new Location
            {
                CityId = adoptionRequestCreateRequest.CityId,
                Address = adoptionRequestCreateRequest.Address,
                PostalCode = adoptionRequestCreateRequest.PostalCode
            };

            var locationInsertResult = await _locationService.CreateLocationAsync(location, cancellationToken);

            if (!locationInsertResult.IsSuccess)
            {
                _logger.LogError("Failed to create a location for the pet with Id: {AdoptionRequestId}", adoptionRequestCreateRequest.Id);
                return Result<bool>.Failure(LocationErrors.CreationFailed);
            }

            var adoptionRequest = _mapper.Map<AdoptionRequest>(adoptionRequestCreateRequest);
            adoptionRequest.RequestDate = DateTime.Now;
            adoptionRequest.Location = locationInsertResult.Value;
            adoptionRequest.RequesterUserId = _userAccessor.GetUserId();
            adoptionRequest.Status = AdoptionRequestStatus.Pending;
            adoptionRequest.Email = _userAccessor.GetEmail();

            await _adoptionRequestRepository.InsertAsync(adoptionRequest, cancellationToken);

            var isAdoptionRequestCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isAdoptionRequestCreated)
            {
                _logger.LogInformation("Created an Adoption Request with Id: {AdoptionRequestId} for UserId: {UserId}", adoptionRequest.Id,
                    adoptionRequest.RequesterUserId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to create an Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequest.Id,
                adoptionRequest.RequesterUserId);
            return Result<bool>.Failure(AdoptionRequestErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Adoption Request for the UserId: {UserId}",
                nameof(AdoptionService), adoptionRequestCreateRequest.RequesterUserId);
            return Result<bool>.Failure(AdoptionRequestErrors.CreationUnexpectedError);
        }
    }

    public async Task<Result<IEnumerable<AdoptionRequest>>> GetAllAdoptionRequestsAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving all adoption requests.");

            var adoptionsRequests = await _adoptionRequestRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved all adoption requests.");
            return Result<IEnumerable<AdoptionRequest>>.Success(adoptionsRequests);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all adoption requests.",
                nameof(AdoptionRequestService));
            return Result<IEnumerable<AdoptionRequest>>.Failure(AdoptionRequestErrors.RetrievalError);
        }
    }

    public async Task<Result<AdoptionRequest>> GetAdoptionRequestByIdAsync(int adoptionRequestId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Adoption Requests with Id: {AdoptionRequestId}", adoptionRequestId);

            var adoptionRequest = await _adoptionRequestRepository.GetByIdAsync(adoptionRequestId, cancellationToken);

            if (adoptionRequest is null)
            {
                _logger.LogWarning("Adoption Request with Id: {AdoptionRequestId} was not found.", adoptionRequestId);
                return Result<AdoptionRequest>.Failure(AdoptionRequestErrors.NotFound(adoptionRequestId));
            }

            _logger.LogInformation("Successfully retrieved Adoption Request with Id: {AdoptionRequestId}", adoptionRequestId);
            return Result<AdoptionRequest>.Success(adoptionRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Adoption Request with Id: {AdoptionRequestId}",
                nameof(AdoptionRequestService), adoptionRequestId);
            return Result<AdoptionRequest>.Failure(AdoptionRequestErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> UpdateAdoptionRequestAsync(AdoptionRequest adoptionRequest, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started updating Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequest.Id,
                adoptionRequest.RequesterUserId);

            await _adoptionRequestRepository.UpdateAsync(adoptionRequest, cancellationToken);
            var adoptionRequestUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (adoptionRequestUpdated)
            {
                _logger.LogInformation("Successfully updated Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequest.Id,
                    adoptionRequest.RequesterUserId);
                return Result<bool>.Success(true);
            }

            _logger.LogWarning("Failed to update Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}. No changes were detected.",
                adoptionRequest.Id, adoptionRequest.RequesterUserId);
            return Result<bool>.Failure(AdoptionErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in the {ServiceName} while attempting to update Adoption Request with Id: {AdoptionRequestId} for the UserId: {UserId}",
                nameof(AdoptionRequestService), adoptionRequest.Id, adoptionRequest.RequesterUserId);
            return Result<bool>.Failure(AdoptionRequestErrors.UpdateUnexpectedError);
        }
    }

    public async Task<Result<bool>> DeleteAdoptionRequestAsync(int adoptionRequestId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started deleting Adoption Request with Id: {AdoptionRequestId}", adoptionRequestId);

            var adoption = await _adoptionRequestRepository.GetByIdAsync(adoptionRequestId, cancellationToken);
            if (adoption is null)
            {
                _logger.LogWarning("Adoption Request with Id: {AdoptionRequestId} was not found.", adoptionRequestId);
                return Result<bool>.Failure(AdoptionRequestErrors.NotFound(adoptionRequestId));
            }

            await _adoptionRequestRepository.DeleteAsync(adoption, cancellationToken);
            var adoptionDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (adoptionDeleted)
            {
                _logger.LogInformation("Successfully deleted Adoption Request with Id: {AdoptionRequestId}", adoptionRequestId);
                return Result<bool>.Success(true);
            }

            _logger.LogWarning("Failed to delete Adoption Request with Id: {AdoptionRequestId}. No changes were detected.", adoptionRequestId);
            return Result<bool>.Failure(AdoptionRequestErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Adoption Request with Id: {AdoptionRequestId}",
                nameof(AdoptionRequestService), adoptionRequestId);
            return Result<bool>.Failure(AdoptionRequestErrors.DeletionUnexpectedError);
        }
    }

    public async Task<Result<List<AdoptionRequestResponse>>> GetAdoptionRequestsByPetIdAsync(int petId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Started retrieving Adoption Requests for PetId: {PetId}", petId);

            var adoptionRequests = await _adoptionRequestRepository.GetByPetIdAsync(petId, cancellationToken);

            if (adoptionRequests == null || !adoptionRequests.Any())
            {
                _logger.LogInformation("No Adoption Requests found for PetId: {PetId}", petId);
                return Result<List<AdoptionRequestResponse>>.Success(new List<AdoptionRequestResponse>());
            }

            var adoptionRequestResponses = _mapper.Map<List<AdoptionRequestResponse>>(adoptionRequests);

            _logger.LogInformation("Successfully retrieved {Count} Adoption Requests for PetId: {PetId}", adoptionRequests.Count(), petId);
            return Result<List<AdoptionRequestResponse>>.Success(adoptionRequestResponses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Adoption Requests for PetId: {PetId}",
                nameof(AdoptionRequestService), petId);
            return Result<List<AdoptionRequestResponse>>.Failure(AdoptionRequestErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> HasUserMadeRequestForPetAsync(string userId, int petId, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _adoptionRequestRepository.ExistsByUserIdAndPetIdAsync(userId, petId, cancellationToken);
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while checking if user {UserId} has made a request for pet {PetId}", userId, petId);
            return Result<bool>.Failure(AdoptionRequestErrors.RetrievalError);
        }
    }
}
